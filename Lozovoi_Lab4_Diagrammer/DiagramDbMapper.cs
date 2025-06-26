using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lozovoi_Lab4_Diagrammer
{
    public class DiagramDbMapper
    {
        static DiagramDbMapper? instance;

        string path;
        Dictionary<int, CustomPrimitive> idMap = new Dictionary<int, CustomPrimitive>();
        DiagramDbMapper() 
        {
            path = string.Empty;
            idMap = new Dictionary<int, CustomPrimitive>();
        }

        public static DiagramDbMapper Instance()
        {
            if(instance == null)
            {
                instance = new DiagramDbMapper();
            }
            return instance;
        }

        private CustomPrimitive LoadPrimitive(Diagram diagram, DiagramDbContext dbcontext, DbPrimitive dbPrimitive)
        {
            CustomPrimitive pr = new CustomRectangle(0, 0, 0, 0); // if error, add an empty rectangle to wrongly mapped primitive (shouldn't ever happen if mapping is correct both ways)
            switch (dbPrimitive.type)
            {
                case "rect":
                    pr = diagram.AddRectangle(dbPrimitive.X, dbPrimitive.Y, (int)dbPrimitive.width, (int)dbPrimitive.height);
                    pr.label = dbPrimitive.label ?? string.Empty;
                    break;
                case "rhombus":
                    pr = diagram.AddRhombus(dbPrimitive.X, dbPrimitive.Y, (int)dbPrimitive.width, (int)dbPrimitive.height);
                    pr.label = dbPrimitive.label ?? string.Empty;
                    break;
                case "label":
                    pr = diagram.AddLabel(dbPrimitive.X, dbPrimitive.Y);
                    pr.label = dbPrimitive.label ?? string.Empty;
                    break;
                case "edge":
                    var links = dbcontext.dbLinks.Where(x => x.linkerId == dbPrimitive.Id).Select(x => x.linkeeId).ToList();
                    CustomPrimitive pr1, pr2;
                    if (!idMap.TryGetValue(links[0], out pr1))
                    {
                        pr1 = LoadPrimitive(diagram, dbcontext, dbcontext.dbPrimitives.Where(x => x.Id == links[0]).ToList()[0]);
                    }
                    if (!idMap.TryGetValue(links[1], out pr2))
                    {
                        pr2 = LoadPrimitive(diagram, dbcontext, dbcontext.dbPrimitives.Where(x => x.Id == links[1]).ToList()[0]);
                    }
                    // pr1 is always above pr2 per Diagram logic, but in db their order may be misaligned because it's based on
                    // insert time
                    // so we may need to rearrange points like Diagram does
                    Point p1 = new Point(dbPrimitive.X, dbPrimitive.Y);
                    Point p2 = new Point(dbPrimitive.X + (int)dbPrimitive.width, dbPrimitive.Y + (int)dbPrimitive.height);
                    if (pr1.Y > pr2.Y)
                    {
                        var t1 = pr1;
                        pr1 = pr2;
                        pr2 = t1;
                    }
                    if (p1.Y > p2.Y)
                    {
                        var t2 = p1;
                        p1 = p2;
                        p2 = t2;
                    }

                    pr = diagram.AddEdge(pr1, pr2, p1, p2);
                    pr.label = dbPrimitive.label ?? string.Empty;
                    break;
            }
            idMap.Add(dbPrimitive.Id, pr);
            return pr;
        }

        public Diagram GetDiagramFromFile(string _path)
        {
            path = _path;
            Diagram diagram = new Diagram();
            DiagramDbContext dbcontext = new DiagramDbContext(path);

            List<DbPrimitive> dbPrimitives = dbcontext.dbPrimitives.ToList();
            idMap.Clear();

            foreach (DbPrimitive dbPr in dbPrimitives)
            {
                if (idMap.ContainsKey(dbPr.Id)) continue;
                LoadPrimitive(diagram, dbcontext, dbPr);
            }
            dbcontext.Dispose();
            return diagram;
        }

        public Diagram CreateEmptyDiagram(string _path)
        {
            path = _path;
            Diagram diagram = new Diagram();
            DiagramDbContext dbcontext = new DiagramDbContext(path);
            dbcontext.Database.EnsureDeleted();
            dbcontext.Database.EnsureCreated();
            dbcontext.Dispose();
            return diagram;
        }

        private DbPrimitive StorePrimitive(DiagramDbContext dbcontext, CustomPrimitive primitive)
        {
            DbPrimitive dbPr = new DbPrimitive();
            dbPr.X = primitive.X;
            dbPr.Y = primitive.Y;
            dbPr.width = primitive.width;
            dbPr.height = primitive.height;

            dbPr.label = primitive.label;
            dbPr.type = primitive.GetCustomType();
            if (idMap.ContainsValue(primitive))
            {
                dbPr.Id = idMap.Where(x => x.Value == primitive).ToList()[0].Key;
                dbcontext.dbPrimitives.Update(dbPr);
                idMap.Remove(dbPr.Id);
            }
            else
            {
                dbcontext.dbPrimitives.Add(dbPr);
            }
            return dbPr;
        }

        // we can't update or delete links (they do so automatically) we only need to insert them manually
        private void InsertLink(DiagramDbContext dbcontext, CustomPrimitive linker, CustomPrimitive linkee)
        {
            DbLink dbLk = new DbLink();
            if (idMap.ContainsValue(linker) && idMap.ContainsValue(linkee))
            {
                dbLk.linkerId = idMap.Where(x => x.Value == linker).ToList()[0].Key;
                dbLk.linkeeId = idMap.Where(x => x.Value == linkee).ToList()[0].Key;
                dbcontext.dbLinks.Add(dbLk);
            }
        }

        private void DeletePrimitive(DiagramDbContext dbcontext, CustomPrimitive primitive)
        {
            DbPrimitive dbPr = new DbPrimitive();
            if (idMap.ContainsValue(primitive))
            {
                dbPr.Id = idMap.Where(x => x.Value == primitive).ToList()[0].Key;
                dbcontext.dbPrimitives.Remove(dbPr);
                idMap.Remove(dbPr.Id);
            }
        }

        public void SaveDiagram(Diagram diagram)
        {
            DiagramDbContext dbcontext = new DiagramDbContext(path);
            dbcontext.Database.EnsureCreated();

            Dictionary<DbPrimitive, CustomPrimitive> newIdMap = new Dictionary<DbPrimitive, CustomPrimitive>();
            Dictionary<CustomPrimitive, List<CustomPrimitive>> newLinks = new Dictionary<CustomPrimitive, List<CustomPrimitive>>();

            foreach (var item in diagram.shapes)
            {
                newIdMap.Add(StorePrimitive(dbcontext, item), item);
            }
            foreach (var item in diagram.edges) // this only stores primitive data. for connections:
                // 1. if edge exists in idMap and on this diagram, then both of its ends exist in both places too (because of Diagram logic), so corresponding connections exist and we don't update them
                // 2. if edge is deleted on diagram, then it will be deleted in idMap loop below and all connections will cascade-delete automatically after SaveChanges
                // 3. if edge didn't exist, only then we need to create a new connection. However we can't insert it immediately as edge can connect new rows that don't have keys until SaveChanges() is called,
                //      meaning we need to keep track of new connections that we need to add
            {
                if (!idMap.ContainsValue(item))
                {
                    List<CustomPrimitive> list = new List<CustomPrimitive>();
                    list.Add(item.pr1);
                    list.Add(item.pr2);
                    newLinks.Add(item, list);
                }
                
                newIdMap.Add(StorePrimitive(dbcontext, item), item);
            }
            foreach (var item in idMap) // if there is something left in old idMap, it means those primitives were deleted on the diagram (but not from db yet)
            {
                DeletePrimitive(dbcontext, item.Value);
            }
            dbcontext.SaveChanges();
            foreach (var item in newIdMap)
            {
                idMap.Add(item.Key.Id, item.Value);
            }
            foreach (var item in newLinks)
            {
                foreach (var item1 in item.Value)
                {
                    InsertLink(dbcontext, item.Key, item1);
                }
            }
            dbcontext.SaveChanges();
            dbcontext.Dispose();
        }
    }
}

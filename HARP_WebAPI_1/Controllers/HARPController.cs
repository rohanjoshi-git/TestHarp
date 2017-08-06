using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HARPDataAccess;
using System.Web.Http.Cors;

namespace HARP_WebAPI_1.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class HARPController : ApiController
    {
        public IEnumerable<HARP> Get()
        {
            using (db_HDPEntities entities = new db_HDPEntities())

            {

                return entities.HARPs.ToList();
            }
        }

        public HARP Get(string id)
        {
            using (db_HDPEntities entities = new db_HDPEntities())

            {

                return entities.HARPs.FirstOrDefault(e => e.NHI == id);
            }
        }

        public HttpResponseMessage Put(string id, [FromBody]HARP harp)
        {
            try
            {
                using (db_HDPEntities entities = new db_HDPEntities())
                {

                    var entity = entities.HARPs.FirstOrDefault(e => e.NHI == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "NHI number: " + id.ToString() + " not found to update.");
                    }
                    else
                    {
                        //entity.NHI = harp.NHI;
                        //entity.Name = harp.Name;
                        entity.RiskPercent = harp.RiskPercent;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
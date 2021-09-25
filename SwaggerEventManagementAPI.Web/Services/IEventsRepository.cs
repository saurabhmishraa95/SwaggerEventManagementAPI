using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerEventManagementAPI.Web.Services
{
    public interface IEventsRepository
    {
        Event Add(Event eventToAdd);
        IEnumerable<Event> GetAll();
        Event GetById(int id);
        void Delete(int id);
    }
}

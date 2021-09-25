using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerEventManagementAPI.Web.Services
{
    public record Event(int Id, DateTime Date, string Location, string Description);

    public class EventsRepository : IEventsRepository
    {
        private List<Event> Events { get; } = new();

        public Event Add(Event eventToAdd)
        {
            Events.Add(eventToAdd);
            return eventToAdd;
        }

        public IEnumerable<Event> GetAll() => Events;

        public Event GetById(int id) => Events.FirstOrDefault(e => e.Id == id);

        public void Delete(int id)
        {
            var eventToDelete = GetById(id);
            if (eventToDelete == null)
                throw new ArgumentException("Event not found for id: ", nameof(id));
            Events.Remove(eventToDelete);
        }
    }
}

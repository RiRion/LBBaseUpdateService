using System.Collections.Generic;

namespace LBBaseUpdateService.BusinessLogic.UpdateService.Models
{
    public abstract class QueueBase<T>
    {
        public List<T> Data { get; } = new List<T>();
        public Queue<T> ListToAdd { get; } = new Queue<T>();
        public Queue<T> ListToUpdate { get; } = new Queue<T>();
        public Queue<int> ListToDelete { get; } = new Queue<int>();
    }
}
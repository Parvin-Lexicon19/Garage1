using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Garage1
{
    [Serializable]
    public class Garage<T> : IEnumerable<T> where T: Vehicle
    {
        private readonly int capacity;
        private T[] vehicles;
        //public Vehicle[] Vehicles
        //{
        //    get
        //    {
        //        return vehicles;
        //    }
        //}
        public Garage(int capacity)
        {
            this.capacity = Math.Max(0, capacity);
            vehicles = new T[this.capacity];
        }

        public int Length => vehicles.Length;
        public bool IsFull => vehicles[vehicles.Length - 1] != null;          
        public virtual bool Add(T item)
        {
            bool successfullyAdded = false;

            for(int i = 0; i < vehicles.Length; i++)
            {
                //if it is null, it has free space to park a vehicle
                if (vehicles[i] is null)
                {
                    vehicles.SetValue(item, i);
                    successfullyAdded = true;
                    break;
                }
            }
            CallUI?.Invoke(this, new GarageEventArgs { vehicles = this.vehicles });
            return successfullyAdded;
        }

        public EventHandler<GarageEventArgs> CallUI;

        public void Remove(int index)
        {
            vehicles.SetValue(null, index);
            for (int i = index; i < vehicles.Length -1; i++)
                vehicles[i] = vehicles[i + 1];
            vehicles[vehicles.Length - 1] = null;
        } 
        
        public void Remove(T vehicle)
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] == vehicle) vehicles[i] = null;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in vehicles)
            {
                if (item is null)
                    break;
                else yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}

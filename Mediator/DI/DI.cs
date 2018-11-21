using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public abstract class DI
    {
        private static DI sharedDI;  

        /// <summary>
        /// Assign Shared property to provided DI if is not assigned previously.
        /// </summary>
        ///
        public static void Initialize(DI di)
        {
            if(sharedDI == null)
            {
                sharedDI = di;
            }
        }

        /// <summary>
        /// Get shared instance by name.
        /// </summary>
        ///
        public static object Get(string name)
        {
            return sharedDI.GetInstance(name);
        }
        protected abstract object GetInstance(string name);


        /// <summary>
        /// Get shared instance by name.
        /// </summary>
        ///

        public static void Set(string name, object instance)
        {
            sharedDI.SetInstance(name, instance);
        }

        protected abstract void SetInstance(string name, object instance);


        /// <summary>
        /// Get shared instance by name.
        /// </summary>
        ///
        protected abstract object CreateInstance(string name);

        public static object Create(string name)
        {
            return sharedDI.CreateInstance(name);
        }
    }
}

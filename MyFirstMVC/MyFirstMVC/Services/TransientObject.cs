using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstMVC.Services
{
    public class ServiceObject
    {
        public ServiceObject()
        {
            Guid = Guid.NewGuid();
        }

        public Guid Guid { get; }
    }

    public class TransientObject : ServiceObject
    {

    }

    public class ScopedObject : ServiceObject
    {

    }

    public class SingletonObject : ServiceObject
    {

    }
}

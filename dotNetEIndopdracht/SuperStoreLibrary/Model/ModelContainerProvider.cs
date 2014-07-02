using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStoreLibrary.Model
{
    class ModelContainerProvider
    {
        private static SuperStoreModelContainer modelContainer;
        public static SuperStoreModelContainer GetInstance()
        {
            if (modelContainer == null)
            {
                modelContainer = new SuperStoreModelContainer();
            }
            return modelContainer;
        }

    }
}

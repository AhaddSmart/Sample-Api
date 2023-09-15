using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CreateService : Attribute
    {
        public bool isCreateService { get; set; }

        public CreateService(bool _isCreateService)
        {
            isCreateService = _isCreateService;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UseProperty : Attribute
    {
        public bool isUseProperty { get; set; }

        public UseProperty(bool _isUseProperty)
        {
            isUseProperty = _isUseProperty;
        }
    }
}


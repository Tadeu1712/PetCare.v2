using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PetCareFinalVersion.Patterns.TemplateMethod
{
    public class ConcreteNotFound : AbstractTemplate
    {
        protected override void SetSuccess()
        {
            Success = false;
        }

        protected override void SetMessage(string aMsg)
        {
            Msg = aMsg;
        }
    }
}

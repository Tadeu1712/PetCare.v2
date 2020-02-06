using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PetCareFinalVersion.Patterns.TemplateMethod
{
    public abstract class AbstractTemplate
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public object TemplateResponse(string aMsg )
        {
            SetSuccess();
            SetMessage(aMsg);
            return ReturnObjectMsg();
            
        }
        
        protected abstract  void SetSuccess();
        protected abstract void SetMessage(string aMsg);


        private object ReturnObjectMsg()
        {
            return new {success = Success, data = Msg };
        }


    }
}

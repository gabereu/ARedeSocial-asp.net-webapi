using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dotnetServer.Application.Exceptions
{
    public class FieldModelException : System.Exception
    {
        public readonly Dictionary<string, string[]> FieldErrors;
        public FieldModelException(ModelStateDictionary modelStateDictionary): base("Field Required Error")
        {
            FieldErrors = new Dictionary<string, string[]>();
            foreach(var modelStateKey in modelStateDictionary.Keys){
                var modelStateEntry = modelStateDictionary[modelStateKey];
                if( modelStateEntry.ValidationState == ModelValidationState.Invalid ){
                    var key = modelStateKey;
                    var errors = new List<string>();
                    foreach (var error in modelStateEntry.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                    FieldErrors.Add(key, errors.ToArray());
                }
            }
        }
    }
}
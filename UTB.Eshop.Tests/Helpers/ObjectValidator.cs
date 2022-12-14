using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace UTB.Eshop.Tests.Helpers
{
    public class ObjectValidator : IObjectModelValidator
    {
        bool _bypassValidation;
        public ObjectValidator(bool bypassValidation)
        {
            _bypassValidation = bypassValidation;
        }

        public void Validate(ActionContext actionContext, ValidationStateDictionary validationState, string prefix, object model)
        {
            if (_bypassValidation == false)
            {
                var context = new ValidationContext(model, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(
                    model, context, results,
                    validateAllProperties: true
                );

                if (!isValid)
                    results.ForEach((r) =>
                    {
                        // Prida validacni chyby do ModelState
                        foreach (var memberName in r.MemberNames)
                            actionContext.ModelState.AddModelError(memberName, r.ErrorMessage);
                    });
            }
        }

    }
}

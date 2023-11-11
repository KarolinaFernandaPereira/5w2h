using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services;

namespace _5w2h.util
{
    public class Validation<TEntity> where TEntity : class
    {
        public static void CopyValidation(ModelStateDictionary modelState, GenericServices<TEntity> service)
        {
            modelState.Clear();
            foreach( var item in service.ValidationDictionary.errors)
            {
                foreach(var erro in service.ValidationDictionary.errors[item.Key]){
                    modelState.AddModelError(item.Key, erro);
                }
            }
        }
    }
}

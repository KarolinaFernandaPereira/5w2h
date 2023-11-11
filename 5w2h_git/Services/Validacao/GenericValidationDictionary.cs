using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validacao
{
    public interface GenericValidationDictionary
    {
        void AddError(string key, string mensagemErro);

        bool isValid { get; }

        Dictionary<string, List<string>> errors { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;

namespace Application.Specifications
{
    public class GetGoalByNameSpec  : BaseSpecification<Goal>
    {
        private string _name;

        public GetGoalByNameSpec(string name) : base(criteria:x=> x.Name==name)
        {
            this._name = name;
        }
    }
}

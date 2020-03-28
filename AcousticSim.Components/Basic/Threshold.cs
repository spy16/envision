using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Basic
{
     class Threshold : Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return "Checks input against a threshold"; }
        }

        public override string ProcessingType
        {
            get { throw new NotImplementedException(); }
        }

        public override void Execute(Blocks.EventDescription e)
        {
            throw new NotImplementedException();
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            throw new NotImplementedException();
        }
    }
}

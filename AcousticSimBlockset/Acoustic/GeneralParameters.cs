using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcousticSimBlockset.Acoustic
{
    public class GeneralParameters : Envision.Blocks.BlockBase
    {
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override string Description
        {
            get { return ""; }
        }

        public override string ProcessingType
        {
            get { return "Acoustic"; }
        }

        public override void Execute(Envision.Blocks.EventDescription e)
        {
            throw new NotImplementedException();
        }

        protected override void CreateNodes(ref Envision.Blocks.BlockBase root)
        {
            throw new NotImplementedException();
        }
    }
}

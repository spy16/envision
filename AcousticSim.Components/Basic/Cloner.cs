using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Envision.Components.Basic
{
    [Serializable]
    public class Cloner : Blocks.BlockBase
    {

        private int outputs = 2;
        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override void Init()
        {
            outputs = Utils.IntegerInput("Number of copies", "Number of clones you want to make", 2);
            if (outputs < 2) outputs = 2;
            CreateNodes(ref root);
        }

        public override string Description
        {
            get { return "Clones same input to multiple outputs"; }
        }

        public override string ProcessingType
        {
            get { return "Signal Routing"; }
        }

        public override void Execute(Blocks.EventDescription e)
        {
            for (int i = 0 ; i < outputs ; i++)
            {
                OutputNodes[i].Object = InputNodes[0].Object;
            }
        }

        protected override void CreateNodes(ref Blocks.BlockBase root)
        {
            this.Picture = Properties.Resources.Cloner;
            InputNodes = new List<Blocks.BlockInputNode>() { new Blocks.BlockInputNode(ref root, "input", "in") };
            OutputNodes = new List<Blocks.BlockOutputNode>();
            for (int i = 0 ; i < outputs ; i++)
            {
                OutputNodes.Add(new Blocks.BlockOutputNode(ref root, "output" + i.ToString(), "out" + i.ToString()));
            }
        }
    }
}

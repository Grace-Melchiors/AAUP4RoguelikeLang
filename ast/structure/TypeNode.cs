using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;

namespace Antlr_language.ast.structure
{
    //Maybe make it abstract to differentiate between array and not?
    public class TypeNode : AbstractNode
    {
        public Enums.Types Type {get; private set;}
        public List<IndividualLayerNode>? mapLayers {get; private set;}
        public bool IsArray => (ArraySizes != null || DimensionRank != 0) ? true : false;
        public List<ExpressionNode>? ArraySizes {get; private set;}
        public int DimensionRank {get; private set;}
        public List<int>? ArrayExpressionDimensionSizes {get;set;}
        public bool OutMostArrayExpression {get;set;} = false;
        public bool WriteArraySizes {get; set;}
        
        public Enums.Types GetDataType() {
            return Type;
        }

        public TypeNode(Enums.Types type, List<IndividualLayerNode>? mapLayers, List<ExpressionNode>? arraySizes, int dimensionRank, bool WriteArraySizes)
        {
            Type = type;
            this.mapLayers = mapLayers;
            ArraySizes = arraySizes;
            DimensionRank = dimensionRank;
            this.WriteArraySizes = WriteArraySizes;
        }


        public Enums.Types GetNodeType () {
            return Type;
        }
        public List<ExpressionNode>? GetArraySizes () {
            return ArraySizes;
        }

    }
}
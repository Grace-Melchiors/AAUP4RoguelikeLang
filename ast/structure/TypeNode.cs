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
        public List<ExpressionNode>? ArraySizes {get;  set;}
        public int DimensionRank {get; set;}
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

        public TypeNode clone () {
            TypeNode node = new TypeNode(Type, null, null, DimensionRank, WriteArraySizes);
            List<IndividualLayerNode>? newMapLayers = null;
            if (mapLayers != null) {
                newMapLayers = new();
                foreach (var elem in mapLayers) {
                    newMapLayers.Add(elem);
                }
            }
            node.mapLayers = newMapLayers;
            List<ExpressionNode>? newArraySizes = null;
            if (ArraySizes != null) {
                newArraySizes = new();
                foreach (var elem in ArraySizes) {
                    newArraySizes.Add(elem);
                }
            }
            node.ArraySizes = newArraySizes;

            List<int>? newArrayExpressionDimensionSizes = null;
            if (ArrayExpressionDimensionSizes != null) {
                newArrayExpressionDimensionSizes = new();
                foreach (var elem in ArrayExpressionDimensionSizes) {
                    newArrayExpressionDimensionSizes.Add(elem);
                }
            }
            node.OutMostArrayExpression = OutMostArrayExpression;
            node.ArrayExpressionDimensionSizes = newArrayExpressionDimensionSizes;

            return node;
        }

    }
}
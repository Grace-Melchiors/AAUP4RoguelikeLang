using System;
using Antlr_language.ast.expression;
using Antlr_language.ast.statement;
using Antlr_language.ast.structure;
using Antlr_language.ast;
using Antlr_language.Content;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Antlr_language
{
    public class TypeIdentifierVisitor : AstBaseVisitorBuilder<string>
    {
        SymbolTable symbolTable = new SymbolTable();
        public override string? Visit(BlockNode context)
        {
            symbolTable.OpenScope();
            base.Visit(context);
            symbolTable.CloseScope();
            return "";
        }

        public override string? Visit(VariableDeclarationNode context)
        {
            symbolTable.EnterSymbol(context.identifier, context);
            return base.Visit(context);
        }
        public override string? Visit(FunctionDeclarationNode context)
        {
            symbolTable.EnterSymbol(context.Identifier, context);
            return base.Visit(context);
        }

        public override string? Visit(MapAccessNode context)
        {
            List<IndividualLayerNode>? mapLayers;
            if (context.factor2.functionCall != null) {
                object? function = symbolTable.RetrieveSymbol(context.factor2.functionCall.IDENTIFIER);
                if (function == null)
                    throw new UndefinedVariableException(context.factor2.functionCall.IDENTIFIER);
                FunctionDeclarationNode funcDeclNode;
                try {
                    funcDeclNode = (FunctionDeclarationNode)function;
                } catch (InvalidCastException e) {
                    throw new UndefinedVariableException(context.factor2.functionCall.IDENTIFIER);
                }
                
                mapLayers = funcDeclNode.Type.mapLayers;

                //We would need to store the return type map with layers.
                //throw new NotImplementedException("Access layers on maps returned from functions, not yet supported.");
                
            } else if (context.factor2.identifier != null) {
                object? map = symbolTable.RetrieveSymbol(context.factor2.identifier);
                if (map == null)
                    throw new UndefinedVariableException(context.factor2.identifier);
                VariableDeclarationNode mapnode = (VariableDeclarationNode)map;
                mapLayers = mapnode.expression?.factor?.mapExpression?.mapLayer.mapLayer;
                
            } else {
                throw new Exception("Malformed MapAccessNode");
            }

            if (mapLayers == null)
                    throw new UndefinedVariableException(context.factor2.identifier);
                List<IndividualLayerNode>? layersMatchingName = new List<IndividualLayerNode>();
                foreach (var layer in mapLayers)
                {
                    if (layer.IDENTIFIER == context.IDENTIFIER) {
                        layersMatchingName.Add(layer);
                    }
                }
                Enums.Types type = layersMatchingName[0].type.Type;
                context.layerType = new TypeNode(type, mapLayers, null);

            return base.Visit(context);
        }

    }
}
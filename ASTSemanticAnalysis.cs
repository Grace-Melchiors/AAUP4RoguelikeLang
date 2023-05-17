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
    public class ASTSemanticAnalysisVisitor : AstBaseVisitorBuilder<Enums.Types?>
    {
        SymbolTable symbolTable = new SymbolTable();
        //SemanticAnalysis 
        public override Enums.Types? Visit(BlockNode context)
        {
            symbolTable.OpenScope();
            base.Visit(context);
            symbolTable.CloseScope();
            return null;
        }

        public override Enums.Types? Visit(VariableDeclarationNode context)
        {
            symbolTable.EnterSymbol(context.identifier, context);
            return base.Visit(context);
        }
        public override Enums.Types? Visit(FunctionDeclarationNode context)
        {
            symbolTable.EnterSymbol(context.Identifier, context);
            return base.Visit(context);
        }

        public override Enums.Types? Visit(ArrayExpressionNode context) {
            
            return base.Visit(context);
        }
        public override Enums.Types? Visit(ExpressionNode expressionNode)
        {
            Enums.Types? expression1Type;
            // If expression1 has a expression1
            if (expressionNode.expression1 != null)
            {
                expression1Type = Visit(expressionNode.expression1);
            }
            // If expression2 has a expression1
            if (expressionNode.expression2 != null)
            {
                expression1Type = Visit(expressionNode.expression2);
            }
            return default(Enums.Types);
            /*Tuple<ExpressionNode, ExpressionNode> expressionNodes = expressionNode.GetExpressionNodes();
            if (expressionNode.GetFactorNode() != null && expressionNodes.Item1 == null && expressionNodes.Item2 == null)
            {
                System.Console.WriteLine("this is a factor");
                Enums.Types? dataType = VisitFactor(expressionNode.GetFactorNode());
                return dataType;
            }



            // This does not work
            else if (expressionNode.GetNumber() != null)
            {
                System.Console.WriteLine("Trying GetNumber");
                return GetDataTypeFromLiteral(expressionNode.GetNumber());

            }

            else if (expressionNodes.Item2 != null)
            {
                Console.WriteLine("Revisiting VisitExpression on Item1");
                Enums.Types? dataType1 = VisitExpression(expressionNodes.Item1);

                Console.WriteLine("Revisiting VisitExpression on Item2");
                Enums.Types? dataType2 = VisitExpression(expressionNodes.Item2);

                if (dataType1 != null && dataType2 != null && dataType1 != dataType2)
                {
                    throw new TypeMismatchException((Enums.Types)dataType1, (Enums.Types)dataType2);
                }
                else if (dataType1 != null)
                {
                    return (Enums.Types)dataType1;
                }

            }
            else
            {
                return VisitExpression(expressionNodes.Item1);
            }

            throw new NotImplementedException("Type not accepted!");
            */
        }


        public override Enums.Types? Visit(MapAccessNode context)
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

        public override Enums.Types? Visit(ProgramNode context)
        {
            foreach (LineNode line in context.lineNodes) {
                if (line.funcDecl != null) {
                    context.FunctionDecls.Add(line);
                } else if (line.statement != null) {
                    context.Statements.Add(line);
                } else {
                    throw new NotImplementedException();
                }
            }
            return base.Visit(context);
        }

    }
}
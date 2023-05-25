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
    public class ASTDecorator : AstBaseVisitorBuilder<TypeNode?>
    {
        SymbolTable symbolTable;
        public ASTDecorator (SymbolTable symbolTable) {
            this.symbolTable = symbolTable;
        }
        public override TypeNode? Visit(BlockNode context)
        {
            symbolTable.OpenScope();
            base.Visit(context);
            symbolTable.CloseScope();
            return null;
        }

        public override TypeNode? Visit(VariableDeclarationNode context)
        {
            if (symbolTable.RetrieveSymbol(context.Identifier) != null)
                throw new VariableAlreadyDefinedException(context.Identifier);
            symbolTable.EnterSymbol(context.Identifier, context);
            return base.Visit(context);
        }

        public override TypeNode? Visit(FunctionDeclarationNode context)
        {
            if (symbolTable.RetrieveSymbol(context.Identifier) != null)
                throw new VariableAlreadyDefinedException(context.Identifier);
            symbolTable.EnterSymbol(context.Identifier, context);
            return base.Visit(context);
        }
        public override TypeNode? Visit(FunctionParamNode context)
        {
            symbolTable.EnterSymbol(context.Identifier, context);
            return base.Visit(context);
        }

        public override TypeNode? Visit(AssignmentNode context) {
            if (context.IDENTIFIER != null) {
                VariableDeclarationNode? decl = (VariableDeclarationNode?)symbolTable.RetrieveSymbol(context.IDENTIFIER);
                if (decl == null)
                    throw new UndefinedVariableException(context.IDENTIFIER);
                
                context.IdentifierType = decl.Type;
            }
            return base.Visit(context);
        }
        
        public override TypeNode? Visit(ExpressionNode context)
        {
            if (context.factor != null) {
                context.type = Visit(context.factor);
                return context.type;
            }
            
            // If expression1 has a expression1
            if (context.expression1 != null)
            {
                context.expression1.type = Visit(context.expression1);
            } else {
                throw new Exception("If Factor is null both expressions must be non null.");
            }
            // If expression2 has a expression1
            if (context.expression2 != null)
            {
                context.expression2.type = Visit(context.expression2);
            } else {
                throw new Exception("If Factor is null both expressions must be non null.");
            }

            if (context.Operator != null) {
                if (context.Operator == Enums.Operators.add || context.Operator == Enums.Operators.sub || context.Operator == Enums.Operators.mult || context.Operator == Enums.Operators.div) {
                    if (context.expression1.type.IsArray || context.expression2.type.IsArray) {
                        if (context.expression1.type.IsArray) {
                            context.type = new TypeNode(Enums.Types.INTEGER, null, null, context.expression1.type.DimensionRank, false);
                            return context.type;
                        } else {
                            context.type = new TypeNode(Enums.Types.INTEGER, null, null, context.expression2.type.DimensionRank, false);
                            return context.type;
                        }
                    } else {
                        if (context.expression1.type.Type == Enums.Types.INTEGER && context.expression2.type.Type == Enums.Types.INTEGER) {
                            context.type = new TypeNode(Enums.Types.INTEGER, null, null, 0, false);
                            return context.type;
                        } else {
                            throw new TypeMismatchException(context.expression1.type.Type, context.expression2.type.Type);
                        }
                    }

                } else if (context.Operator == Enums.Operators.eq || context.Operator == Enums.Operators.neq || context.Operator == Enums.Operators.greater || context.Operator == Enums.Operators.less || context.Operator == Enums.Operators.geq || context.Operator == Enums.Operators.leq) {
                    if (context.expression1.type.IsArray || context.expression2.type.IsArray) {
                        if (context.expression1.type.IsArray) {
                            context.type = new TypeNode(Enums.Types.BOOL, null, null, context.expression1.type.DimensionRank, false);
                            return context.type;
                        } else {
                            context.type = new TypeNode(Enums.Types.BOOL, null, null, context.expression2.type.DimensionRank, false);
                            return context.type;
                        }
                    } else {
                        if (context.expression1.type.Type == Enums.Types.INTEGER && context.expression2.type.Type == Enums.Types.INTEGER) {
                            context.type = new TypeNode(Enums.Types.BOOL, null, null, 0, false);
                            return context.type;
                        } else {
                            throw new TypeMismatchException(context.expression1.type.Type, context.expression2.type.Type);
                        }
                    }
                } else if (context.Operator == Enums.Operators.and || context.Operator == Enums.Operators.or) {
                    if (context.expression1.type.IsArray || context.expression2.type.IsArray) {
                        if (context.expression1.type.IsArray) {
                            context.type = new TypeNode(Enums.Types.BOOL, null, null, context.expression1.type.DimensionRank, false);
                            return context.type;
                        } else {
                            context.type = new TypeNode(Enums.Types.BOOL, null, null, context.expression2.type.DimensionRank, false);
                            return context.type;
                        }
                    } else {
                        if (context.expression1.type.Type == Enums.Types.BOOL && context.expression2.type.Type == Enums.Types.BOOL) {
                            context.type = new TypeNode(Enums.Types.BOOL, null, null, 0, false);
                            return context.type;
                        } else {
                            throw new TypeMismatchException(context.expression1.type.Type, context.expression2.type.Type);
                        }
                    }
                } else {
                    throw new InvalidOperator((Enums.Operators)context.Operator);
                }
            } else {
                throw new Exception("Must have operator if factor is null.");
            }
        }

        public override TypeNode? Visit(FactorNode context)
        {
            TypeNode? result;
            if (context.parenthesizedExpression != null) {
                result = Visit(context.parenthesizedExpression);
            } else if (context.constant != null) {
                result =  Visit(context.constant);
            } else if (context.factor2 != null) {
                result =  Visit(context.factor2);
            } else if (context.arrayExpressionsNode != null) {
                result =  Visit(context.arrayExpressionsNode);
            } else if (context.mapExpression != null) {
                result =  Visit(context.mapExpression);
            } else if (context.arrayAccess != null) {
                result =  Visit(context.arrayAccess);
            } else if (context.mapAccess != null) {
                result =  Visit(context.mapAccess);
            } else {
                throw new NotImplementedException();
            }
            context.type = result;
            return result;
        }

        public override TypeNode? Visit(ConstantNode context) {
            if (context.boolean != null) {
                context.type = new TypeNode(Enums.Types.BOOL, null, null, 0, false);
            } else if (context.integer != null) {
                context.type = new TypeNode(Enums.Types.INTEGER, null, null, 0, false);
            } else {
                throw new Exception("Malformed canstantnode.");
            }
            return context.type;
        }
        public override TypeNode? Visit(Factor2Node context) {
            if (context.identifier != null) {
                VariableDeclarationNode? decl = (VariableDeclarationNode?)symbolTable.RetrieveSymbol(context.identifier);
                if (decl == null)
                    throw new UndefinedVariableException(context.identifier);
                
                context.type = decl.Type;
            } else if (context.functionCall != null) {
                FunctionDeclarationNode? decl = (FunctionDeclarationNode?)symbolTable.RetrieveSymbol(context.functionCall.IDENTIFIER);
                if (context.functionCall.LIBRARY == null) {

                    if (decl == null) {
                        throw new UndefinedVariableException(context.identifier ?? "");
                    }
                    context.type = decl.Type;
                } else {
                    context.type = null;
                }
            } else {
                throw new Exception("Malformed factor2node.");
            }
            base.Visit(context);
            return context.type;
        }
        public override TypeNode? Visit(ArrayExpressionNode context) {
            try {
                if (context.expressions.Count <= 0)
                    throw new Exception("Empty array expression.");
                List<int> DimensionSizes = new List<int>();
                List<TypeNode?> expressionsTypes = new List<TypeNode?>();
                
                TypeNode? FirstType = Visit(context.expressions.First());
                if (FirstType == null)
                    throw new Exception("Unknown first type in array expression.");
                context.type = new TypeNode(FirstType.Type, null, null, 0, false);
                FirstType.OutMostArrayExpression = false;
                for (int i = 1; i < context.expressions.Count; i++)
                {
                    var expr = context.expressions[i];
                    TypeNode? currentType = Visit(expr);
                    if (currentType == null)
                        throw new Exception("No type on first expression");

                    if (currentType.Type != context.type.Type)
                        throw new TypeMismatchException(context.type.Type, currentType.Type);
                    
                    expressionsTypes.Add(currentType);
                    currentType.OutMostArrayExpression = false;

                    if (currentType.ArrayExpressionDimensionSizes is null != FirstType.ArrayExpressionDimensionSizes is null) {
                        if(currentType.ArrayExpressionDimensionSizes is null) {
                            throw new Exception("Expected arrayExpression, but got something else.");
                        } else {
                            throw new Exception("Expected a value, but got an arrayExpression.");
                        }
                    }
                    if (currentType.ArrayExpressionDimensionSizes is null || FirstType.ArrayExpressionDimensionSizes is null) {
                        
                    } else {
                        if (currentType.ArrayExpressionDimensionSizes.Count == FirstType.ArrayExpressionDimensionSizes.Count) {
                            for (int j = 0; j < currentType.ArrayExpressionDimensionSizes.Count; j++) {
                                if (currentType.ArrayExpressionDimensionSizes[j] == FirstType.ArrayExpressionDimensionSizes[j]) {
                                    DimensionSizes.Add(currentType.ArrayExpressionDimensionSizes[j]);
                                    
                                } else {
                                    throw new Exception("Dimension " + j + " is not the same size.");
                                }
                            }
                        }
                        else {
                            throw new Exception("Not same number of dimensions in all arrayExpressions.");
                        }
                    }
                }
                DimensionSizes.Insert(0, context.expressions.Count);
                context.type.OutMostArrayExpression = true;

                context.type.ArrayExpressionDimensionSizes = DimensionSizes;
            } catch (Exception e) {
                context.type = null;
                throw e;
            }
            return context.type;
        }


        public override TypeNode? Visit(MapAccessNode context)
        {
            List<IndividualLayerNode>? mapLayers;
            if (context.factor2.functionCall != null) {
                object? function = symbolTable.RetrieveSymbol(context.factor2.functionCall.IDENTIFIER);
                if (function == null)
                    throw new UndefinedVariableException(context.factor2.functionCall.IDENTIFIER);
                FunctionDeclarationNode funcDeclNode;
                try {
                    funcDeclNode = (FunctionDeclarationNode)function;
                } catch (InvalidCastException) {
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
                throw new UndefinedVariableException(context.factor2.identifier ?? "");
            List<IndividualLayerNode>? layersMatchingName = new List<IndividualLayerNode>();
            foreach (var layer in mapLayers)
            {
                if (layer.IDENTIFIER == context.IDENTIFIER) {
                    layersMatchingName.Add(layer);
                }
            }
            Enums.Types type = layersMatchingName[0].LayerType.Type;
            context.layerType = new TypeNode(type, mapLayers, null, 0, false);

            return context.layerType;
        }

        public override TypeNode? Visit(ProgramNode context)
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
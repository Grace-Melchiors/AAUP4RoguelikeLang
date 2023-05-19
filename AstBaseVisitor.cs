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
    public partial class AstBaseVisitorBuilder<TResult>
    {
        
        public virtual TResult? Visit(ProgramNode context) {
            foreach (var library in context.libraryNodes) {
                Visit(library);
            }
            foreach (var line in context.lineNodes) {
                Visit(line);
            }
            return default(TResult);
        }
        public virtual TResult? Visit(LibraryNode context) {
            return default(TResult);
        }
        public virtual TResult? Visit(LineNode context) {
            if (context.statement != null)
                Visit(context.statement);
            if (context.funcDecl != null)
                Visit(context.funcDecl);
            return default(TResult);
        }
        public virtual TResult? Visit(StatementNode context) {
            if (context.varDecl != null) {
                Visit(context.varDecl);
            } else if (context.assign != null) {
                Visit(context.assign);
            } else if (context.expression != null) {
                Visit(context.expression);
            } else if (context.returnStatement != null) {
                Visit(context.returnStatement);
            } else if (context.block != null) {
                Visit(context.block);
            } else if (context.ifNode != null) {
                Visit(context.ifNode);
            } else if (context.whileNode != null) {
                Visit(context.whileNode);
            } else if (context.forNode != null) {
                Visit(context.forNode);
            } else if (context.chance != null) {
                Visit(context.chance);
            } else if (context.statements != null) {
                Visit(context.statements);
            } else {
                throw new NotImplementedException();
            }
            return default(TResult);
        }
        public virtual TResult? Visit(VariableDeclarationNode context) {
            if (context.Type != null)
                Visit(context.Type);
            if (context.expression != null)
                Visit(context.expression);
            return default(TResult);
        }
        public virtual TResult? Visit(AssignmentNode context) {
            if (context.ArrayIndicies != null)
                foreach (var expression in context.ArrayIndicies)
                    Visit(expression);
            if (context.expression != null)
                Visit(context.expression);
            return default(TResult);
        }
        public virtual TResult? Visit(ReturnStatementNode context) {
            if (context.expression != null)
                Visit(context.expression);
            return default(TResult);
        }
        public virtual TResult? Visit(IfNode context) {
            if (context.expression != null)
                Visit(context.expression);
            if (context.block != null)
                Visit(context.block);
            if (context.elseBlock != null)
                Visit(context.elseBlock);
            return default(TResult);
        }
        public virtual TResult? Visit(WhileNode context) {
            if (context.expression != null)
                Visit(context.expression);
            if (context.block != null)
                Visit(context.block);
            return default(TResult);
        }
        public virtual TResult? Visit(ForNode context) {
            if (context.declaration != null)
                Visit(context.declaration);
            if (context.expression != null)
                Visit(context.expression);
            if (context.assignment != null)
                Visit(context.assignment);
            if (context.block != null)
                Visit(context.block);
            return default(TResult);
        }
        public virtual TResult? Visit(ChanceNode context) {
            if (context.weights != null)
                foreach (var expression in context.weights)
                    Visit(expression);
            if (context.blocks != null)
                foreach (var block in context.blocks)
                    Visit(block);
            return default(TResult);
        }
        public virtual TResult? Visit(StatementsNode context) {
            if (context.statements != null)
                foreach (var statement in context.statements)
                    Visit(statement);
            return default(TResult);
        }
        public virtual TResult? Visit(FunctionDeclarationNode context) {
            if (context.Type != null)
                Visit(context.Type);
            foreach (var funcParam in context.funcParams) {
                Visit(funcParam);
            }
            if (context.body != null)
                Visit(context.body);
            return default(TResult);
        }
        public virtual TResult? Visit(TypeNode context) {
            if (context.mapLayers != null)
                foreach (var layer in context.mapLayers)
                    Visit(layer);
            if (context.ArraySizes != null)
                foreach (var expression in context.ArraySizes)
                    Visit(expression);
            return default(TResult);
        }
        public virtual TResult? Visit(ExpressionNode context) {
            if (context.expression1 != null)
                Visit(context.expression1);
            if (context.expression2 != null)
                Visit(context.expression2);
            if (context.factor != null)
                Visit(context.factor);
            return default(TResult);
        }
        public virtual TResult? Visit(FactorNode context) {
            if (context.parenthesizedExpression != null)
                Visit(context.parenthesizedExpression);
            if (context.constant != null)
                Visit(context.constant);
            if (context.factor2 != null)
                Visit(context.factor2);
            if (context.arrayExpressionsNode != null)
                Visit(context.arrayExpressionsNode);
            if (context.mapExpression != null)
                Visit(context.mapExpression);
            if (context.arrayAccess != null)
                Visit(context.arrayAccess);
            if (context.mapAccess != null)
                Visit(context.mapAccess);
            return default(TResult);
        }
        public virtual TResult? Visit(ConstantNode context) {
            return default(TResult);
        }
        public virtual TResult? Visit(Factor2Node context) {
            if (context.functionCall != null)
                Visit(context.functionCall);
            return default(TResult);
        }
        public virtual TResult? Visit(FunctionCallNode context) {
            if (context.parameters != null)
                foreach (var param in context.parameters)
                    Visit(param);
            return default(TResult);
        }
        public virtual TResult? Visit(ArrayExpressionNode context) {
            if (context.expressions != null)
                foreach (var expression in context.expressions)
                    Visit(expression);
            return default(TResult);
        }
        public virtual TResult? Visit(MapExpressionNode context) {
            if (context.arrayDimensions != null)
                Visit(context.arrayDimensions);
            if (context.mapLayer != null)
                Visit(context.mapLayer);
            return default(TResult);
        }
        public virtual TResult? Visit(ArrayDimensionsNode context) {
            if (context.expressions != null)
                foreach (var expression in context.expressions)
                    Visit(expression);
            return default(TResult);
        }
        public virtual TResult? Visit(MapLayerNode context) {
            if (context.mapLayer != null)
                foreach (var IndividualLayer in context.mapLayer)
                    Visit(IndividualLayer);
            return default(TResult);
        }
        public virtual TResult? Visit(IndividualLayerNode context) {
            if (context.LayerType != null)
                Visit(context.LayerType);
            if (context.Expression != null)
                Visit(context.Expression);
            return default(TResult);
        }

        public virtual TResult? Visit(ArrayAccessNode context) {
            if (context.factor2 != null)
                Visit(context.factor2);
            if (context.indicies != null)
                Visit(context.indicies);
            return default(TResult);
        }
        public virtual TResult? Visit(MapAccessNode context) {
            if (context.factor2 != null)
                Visit(context.factor2);
            if (context.arrayDimensions != null)
                Visit(context.arrayDimensions);
            if (context.layerType != null)
                Visit(context.layerType);
            return default(TResult);
        }
        public virtual TResult? Visit(FunctionParamNode context) {
            if (context.Type != null)
                Visit(context.Type);
            return default(TResult);
        }
        public virtual TResult? Visit(BlockNode context) {
            foreach (var statement in context.statementNodes)
                Visit(statement);
            return default(TResult);
        }
    }
}
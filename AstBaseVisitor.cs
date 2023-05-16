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
                Visit((dynamic)library);
            }
            foreach (var line in context.lineNodes) {
                Visit((dynamic)line);
            }
            return default(TResult);
        }
        public virtual TResult? Visit(LibraryNode context) {
            return default(TResult);
        }
        public virtual TResult? Visit(LineNode context) {
            if (context.statement != null)
                Visit((dynamic)context.statement);
            if (context.funcDecl != null)
                Visit((dynamic)context.funcDecl);
            return default(TResult);
        }
        //Mangler
        public virtual TResult? Visit(StatementNode context) {
            if (context.varDecl != null) {
                Visit((dynamic)context.varDecl);
            } else if (context.assign != null) {
                Visit((dynamic)context.assign);
            } else if (context.expression != null) {
                Visit((dynamic)context.expression);
            } else if (context.returnStatement != null) {
                Visit((dynamic)context.returnStatement);
            } else if (context.block != null) {
                Visit((dynamic)context.block);
            } else if (context.ifNode != null) {
                Visit((dynamic)context.ifNode);
            } else if (context.whileNode != null) {
                Visit((dynamic)context.whileNode);
            } else if (context.chance != null) {
                Visit((dynamic)context.chance);
            } else if (context.statements != null) {
                Visit((dynamic)context.statements);
            } else {
                throw new NotImplementedException();
            }
            return default(TResult);
        }
        public virtual TResult? Visit(VariableDeclarationNode context) {
            if (context.Type != null)
                Visit((dynamic)context.Type);
            if (context.expression != null)
                Visit((dynamic)context.expression);
            return default(TResult);
        }
        public virtual TResult? Visit(AssignmentNode context) {
            if (context.ArrayIndicies != null)
                foreach (var expression in context.ArrayIndicies)
                    Visit((dynamic)expression);
            if (context.expression != null)
                Visit((dynamic)context.expression);
            return default(TResult);
        }
        public virtual TResult? Visit(ReturnStatementNode context) {
            if (context.expression != null)
                Visit((dynamic)context.expression);
            return default(TResult);
        }
        public virtual TResult? Visit(IfNode context) {
            if (context.expression != null)
                Visit((dynamic)context.expression);
            if (context.block != null)
                Visit((dynamic)context.block);
            if (context.elseBlock != null)
                Visit((dynamic)context.elseBlock);
            return default(TResult);
        }
        public virtual TResult? Visit(WhileNode context) {
            if (context.expression != null)
                Visit((dynamic)context.expression);
            if (context.block != null)
                Visit((dynamic)context.block);
            return default(TResult);
        }
        public virtual TResult? Visit(ChanceNode context) {
            if (context.weights != null)
                foreach (var expression in context.weights)
                    Visit((dynamic)expression);
            if (context.blocks != null)
                foreach (var block in context.blocks)
                    Visit((dynamic)block);
            return default(TResult);
        }
        public virtual TResult? Visit(StatementsNode context) {
            if (context.statements != null)
                foreach (var statement in context.statements)
                    Visit((dynamic)statement);
            return default(TResult);
        }
        public virtual TResult? Visit(FunctionDeclarationNode context) {
            if (context.Type != null)
                Visit((dynamic)context.Type);
            foreach (var funcParam in context.funcParams) {
                Visit((dynamic)funcParam);
            }
            if (context.body != null)
                Visit((dynamic)context.body);
            return default(TResult);
        }
        public virtual TResult? Visit(TypeNode context) {
            if (context.ArraySizes != null)
                foreach (var expression in context.ArraySizes)
                    Visit((dynamic)expression);
            return default(TResult);
        }
        public virtual TResult? Visit(ExpressionNode context) {
            if (context.expression1 != null)
                Visit((dynamic)context.expression1);
            if (context.expression2 != null)
                Visit((dynamic)context.expression2);
            if (context.factor != null)
                Visit((dynamic)context.factor);
            return default(TResult);
        }
        public virtual TResult? Visit(FactorNode context) {
            if (context.parenthesizedExpression != null)
                Visit((dynamic)context.parenthesizedExpression);
            if (context.constant != null)
                Visit((dynamic)context.constant);
            if (context.factor2 != null)
                Visit((dynamic)context.factor2);
            if (context.arrayExpressionsNode != null)
                Visit((dynamic)context.arrayExpressionsNode);
            if (context.mapExpression != null)
                Visit((dynamic)context.mapExpression);
            if (context.arrayAccess != null)
                Visit((dynamic)context.arrayAccess);
            if (context.mapAccess != null)
                Visit((dynamic)context.mapAccess);
            return default(TResult);
        }
        public virtual TResult? Visit(ConstantNode context) {
            return default(TResult);
        }
        public virtual TResult? Visit(Factor2Node context) {
            if (context.functionCall != null)
                Visit((dynamic)context.functionCall);
            return default(TResult);
        }
        public virtual TResult? Visit(FunctionCallNode context) {
            if (context.parameters != null)
                foreach (var param in context.parameters)
                    Visit((dynamic)param);
            return default(TResult);
        }
        public virtual TResult? Visit(ArrayExpressionNode context) {
            if (context.expressions != null)
                foreach (var expression in context.expressions)
                    Visit((dynamic)expression);
            return default(TResult);
        }
        public virtual TResult? Visit(MapExpressionNode context) {
            if (context.arrayDimensions != null)
                Visit((dynamic)context.arrayDimensions);
            if (context.mapLayer != null)
                Visit((dynamic)context.mapLayer);
            return default(TResult);
        }
        public virtual TResult? Visit(ArrayDimensionsNode context) {
            if (context.expressions != null)
                foreach (var expression in context.expressions)
                    Visit((dynamic)expression);
            return default(TResult);
        }
        public virtual TResult? Visit(MapLayerNode context) {
            if (context.mapLayer != null)
                foreach (var IndividualLayer in context.mapLayer)
                    Visit((dynamic)IndividualLayer);
            return default(TResult);
        }
        public virtual TResult? Visit(IndividualLayerNode context) {
            if (context.type != null)
                Visit((dynamic)context.type);
            if (context.expression != null)
                Visit((dynamic)context.expression);
            return default(TResult);
        }

        public virtual TResult? Visit(ArrayAccessNode context) {
            if (context.factor2 != null)
                Visit((dynamic)context.factor2);
            if (context.indicies != null)
                Visit((dynamic)context.indicies);
            return default(TResult);
        }
        public virtual TResult? Visit(MapAccessNode context) {
            if (context.factor2 != null)
                Visit((dynamic)context.factor2);
            if (context.arrayDimensions != null)
                Visit((dynamic)context.arrayDimensions);
            if (context.layerType != null)
                Visit((dynamic)context.layerType);
            return default(TResult);
        }
        public virtual TResult? Visit(FunctionParamNode context) {
            if (context.Type != null)
                Visit((dynamic)context.Type);
            return default(TResult);
        }
        public virtual TResult? Visit(BlockNode context) {
            foreach (var statement in context.statementNodes)
                Visit((dynamic)statement);
            return default(TResult);
        }
    }
}
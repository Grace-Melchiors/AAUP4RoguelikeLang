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
    public class AstBuilder : VestaBaseVisitor<AbstractNode>
    {
        public override AbstractNode VisitProgram([NotNull] VestaParser.ProgramContext context)
        {
            ProgramNode AST = new ProgramNode();
            var libraryArr = context.library().ToArray();
            for (int i = 0; i < libraryArr.Length; i++)
            {
                AST.AddLibraryNode((LibraryNode)Visit(libraryArr[i]));
            }
            var lineArr = context.line().ToArray();
            for (int i = 0; i < lineArr.Length; i++)
            {
                AST.AddLineNode((LineNode)Visit(lineArr[i]));
            }

            return AST;
        }
        public override AbstractNode VisitLibrary([NotNull] VestaParser.LibraryContext context)
        {
            LibraryNode result;
            if (context.IDENTIFIER().GetText() == "Stdlib") {
                result = new Stdlib();
            } else {
                throw new NotImplementedException();
            }

            return result;
        }
        public override AbstractNode VisitLine([NotNull] VestaParser.LineContext context)
        {
            var statement = context.statement();
            var functionDecl = context.functionDecl();
            LineNode result;
            if (statement != null) {
                result = new LineNode((StatementNode)Visit(statement), null);
            } else if (functionDecl != null) {
                result = new LineNode(null, (FunctionDeclarationNode)Visit(functionDecl));
            } else {
                throw new Exception();
            }
            return result;
        }

        public override AbstractNode VisitStatement([NotNull] VestaParser.StatementContext context)
        {
            StatementNode result;
            var VariableDecl = context.varDecl();
            var assignment = context.assignment();
            var expression = context.expression();
            //var returnStmt = context.returnStmt();
            var block = context.block();
            var ifStatement = context.ifStatement();
            var whileStatement = context.whileStatement();
            var forStatement = context.forStatement();  //Maybe something funky here, as there is no for statement in statement make it a while?
            var chanceStatement = context.chance();


            if (VariableDecl != null) {
                result = new StatementNode((VariableDeclarationNode)Visit(VariableDecl), null, null, null, null, null, null, null, null, null);
            } else if (assignment != null) {
                result = new StatementNode(null, (AssignmentNode)Visit(assignment), null, null, null, null, null, null, null, null);
            } else if (expression != null) {
                result = new StatementNode(null, null, (ExpressionNode)Visit(expression), null, null, null, null, null, null, null);
            //} else if (returnStmt != null) {
                //result = new StatementNode(null, null, null, (ReturnStatementNode)Visit(returnStmt), null, null, null, null, null, null);
            } else if (block != null) {
                result = new StatementNode(null, null, null, null, (BlockNode)Visit(block), null, null, null, null, null);
            } else if (ifStatement != null) {
                result = new StatementNode(null, null, null, null, null, (IfNode)Visit(ifStatement), null, null, null, null);
            } else if (whileStatement != null) {
                result = new StatementNode(null, null, null, null, null, null, (WhileNode)Visit(whileStatement), null, null, null);
            } else if (forStatement != null) {
                result = new StatementNode(null, null, null, null, null, null, null, (ForNode)Visit(forStatement), null, null);
            } else if (chanceStatement != null) {
                result = new StatementNode(null, null, null, null, null, null, null, null, (ChanceNode)Visit(chanceStatement), null);
            } else {
                throw new NotImplementedException();
            }
            return result;
        }

        public override AbstractNode VisitIfStatement([NotNull] VestaParser.IfStatementContext context)
        {
            return new IfNode((ExpressionNode)Visit(context.expression()), (BlockNode)Visit(context.block().ToArray()[0]), context.block().ToArray().Length == 2 ? (BlockNode)Visit(context.block().ToArray()[1]) : null);
        }
        public override AbstractNode VisitWhileStatement([NotNull] VestaParser.WhileStatementContext context)
        {
            return new WhileNode((ExpressionNode)Visit(context.expression()), (BlockNode)Visit(context.block()));
        }
        public override AbstractNode VisitForStatement([NotNull] VestaParser.ForStatementContext context)
        {
            ForNode result = new ForNode((VariableDeclarationNode)Visit(context.varDecl()), (ExpressionNode)Visit(context.expression()),(AssignmentNode)Visit(context.assignment()),(BlockNode)Visit(context.block()));
            return result;
        }

        public override AbstractNode VisitChance([NotNull] VestaParser.ChanceContext context)
        {
            
            List<ExpressionNode> expressions = ContextToNode<ExpressionNode>(context.expression().ToArray());
            List<BlockNode> blocks = ContextToNode<BlockNode>(context.block().ToArray());
            return new ChanceNode(expressions, blocks);

        }
        
        public override AbstractNode VisitVarDecl([NotNull] VestaParser.VarDeclContext context)
        {
            return base.VisitVarDecl(context);
        }
        public override AbstractNode VisitVarDeclaration([NotNull] VestaParser.VarDeclarationContext context)
        {
            VariableDeclarationNode result;
            
            result = new VariableDeclarationNode((TypeNode)Visit(context.identifierType()), context.IDENTIFIER().GetText(), null);

            return result;
            //return base.VisitVarDeclaration(context);
        }
        public override AbstractNode VisitVarInitialization([NotNull] VestaParser.VarInitializationContext context)
        {
            VariableDeclarationNode result;
            var typeContext = context.allType();
            TypeNode type = (TypeNode)Visit(typeContext);
            var assignmentContext = context.assignment();
            result = new VariableDeclarationNode(type, assignmentContext.IDENTIFIER().GetText(), (ExpressionNode)Visit(assignmentContext.expression()));
            return result;
        }

        public override AbstractNode VisitAssignment([NotNull] VestaParser.AssignmentContext context)
        {
            AssignmentNode result;
            var expressions = context.arrayDimensions()?.expression().ToArray();
            if (expressions != null) {
                List<ExpressionNode> expressionNodes= new List<ExpressionNode>();
                foreach (var val in expressions)
                {
                    expressionNodes.Add((ExpressionNode)Visit(val));
                }
                result = new AssignmentNode(context.IDENTIFIER().GetText(),expressionNodes, (ExpressionNode)Visit(context.expression()));
            } else {
                result = new AssignmentNode(context.IDENTIFIER().GetText(), null, (ExpressionNode)Visit(context.expression()));
            }

            return result;
        }


        /// ---  Function  ---  ///
        public override AbstractNode VisitFunctionDecl([NotNull] VestaParser.FunctionDeclContext context)
        {
            FunctionDeclarationNode result;
            List<FunctionParamNode> parameters = new List<FunctionParamNode>();
            var parametersContext = context.funcParams()?.parameter().ToArray();
            if (parametersContext != null) {
                foreach (var parameter in parametersContext) {
                    parameters.Add((FunctionParamNode)Visit(parameter));
                }
            }
            result = new FunctionDeclarationNode((TypeNode)Visit(context.returnType()), context.IDENTIFIER().GetText(), parameters, (BlockNode)Visit(context.funcBody()));
            

            return result;
        }

        public override AbstractNode VisitParameter([NotNull] VestaParser.ParameterContext context)
        {
            FunctionParamNode result;
            result = new FunctionParamNode((TypeNode)Visit(context.parameterType()), context.IDENTIFIER().GetText());
            return result;
        }

        public override AbstractNode VisitReturnType([NotNull] VestaParser.ReturnTypeContext context)
        {
            TypeNode result;
            
            if (context.verboseComplextype() != null) {
                result = (TypeNode)Visit(context.verboseComplextype());
            } else if (context.TYPE() != null) {
                Enums.Types type;
                if (context.TYPE().GetText() == "int") {
                    type = Enums.Types.INTEGER;
                } else if (context.TYPE().GetText() == "bool") {
                    type = Enums.Types.BOOL;
                } else {
                    throw new NotImplementedException();
                }
                var array = context.parameterArr();
                if (array == null) {
                    result = new TypeNode(type, null, null, 0, false);
                } else {
                    int arrRank = array.paramaterArrayDenoter().ToArray().Length + 1;
                    result = new TypeNode(type, null, null, arrRank, false);
                }
                
            } else {
                throw new NotImplementedException();
            }

            return result;
        }
        public override AbstractNode VisitParameterType([NotNull] VestaParser.ParameterTypeContext context)
        {
            TypeNode result;
            
            if (context.TYPE() != null) {
                Enums.Types type;
                if (context.TYPE().GetText() == "int") {
                    type = Enums.Types.INTEGER;
                } else if (context.TYPE().GetText() == "bool") {
                    type = Enums.Types.BOOL;
                } else {
                    throw new NotImplementedException();
                }
                var array = context.parameterArr();
                if (array == null) {
                    result = new TypeNode(type, null, null, 0, false);
                } else {
                    int arrRank = array.paramaterArrayDenoter().ToArray().Length + 1;
                    result = new TypeNode(type, null, null, arrRank, false);
                }
            } else {
                throw new NotImplementedException();
            }

            return result;
        }

        public override AbstractNode VisitVerboseComplextype([NotNull] VestaParser.VerboseComplextypeContext context)
        {
            TypeNode result;
            result = new TypeNode(Enums.Types.MAP, ContextToNode<IndividualLayerNode>(context.mapLayer().individualLayer().ToArray()), null, 0, true);

            return result;
        }

        public override AbstractNode VisitFuncBody([NotNull] VestaParser.FuncBodyContext context)
        {
            List<StatementNode> statements = ContextToNode<StatementNode>(context.statement().ToArray());
            statements.Add(new StatementNode(null, null, null, (ReturnStatementNode)Visit(context.returnStmt()), null, null, null, null, null, null));
            return new BlockNode(statements);
        }

        public override AbstractNode VisitReturnStmt([NotNull] VestaParser.ReturnStmtContext context)
        {
            ReturnStatementNode result;

            result = new ReturnStatementNode((ExpressionNode)Visit(context.expression()));

            return result;
        }

        public override AbstractNode VisitAllType([NotNull] VestaParser.AllTypeContext context)
        {
            TypeNode result;
            if (context.COMPLEXTYPE() != null) {
                Enums.Types type;
                if (context.COMPLEXTYPE().GetText() == "map") {
                    type = Enums.Types.MAP;
                } else {
                    throw new NotImplementedException();
                }
                result = new TypeNode(type, null, null, 0, false);
            } else if (context.identifierType() != null) {
                result = (TypeNode)Visit(context.identifierType());
            } else {
                throw new NotImplementedException();
            }
            return result;
        }
        public override AbstractNode VisitIdentifierType([NotNull] VestaParser.IdentifierTypeContext context)
        {
            TypeNode result;
            Enums.Types type;
            var TypeContext = context.TYPE();
            if (TypeContext.GetText() == "int") {
                type = Enums.Types.INTEGER;
            } else if (TypeContext.GetText() == "bool") {
                type = Enums.Types.BOOL;
            } else {
                throw new NotImplementedException();
            }
            result = new TypeNode(type, null, null, 0, true);
            //Array check expressions here!
            //bool IsArray = false;
            try {
                var arraySizes = context.arrayDimensions().expression().ToArray();
                List<ExpressionNode> sizes = new();
                if (arraySizes.Length != 0) {
                    //IsArray = true;
                    foreach (var ArraySize in arraySizes) {
                        sizes.Add((ExpressionNode)Visit(ArraySize));
                    }
                    result = new TypeNode(type, null, sizes, sizes.Count, false);
                }
            } catch (NullReferenceException e) {
                //System.Console.WriteLine("Error: " + e.Message);
            }
            return result;
        }

        public override AbstractNode VisitFactorExpression([NotNull] VestaParser.FactorExpressionContext context)
        {
            ExpressionNode result;

            result = new ExpressionNode(Enums.Operators.none, null,null, (FactorNode)Visit(context.factor()));
            return result;
        }
        public override AbstractNode VisitNotExpression([NotNull] VestaParser.NotExpressionContext context)
        {
            ExpressionNode result;

            result = new ExpressionNode(Enums.Operators.not, null,null, (FactorNode)Visit(context.factor()));
            return result;
        }
        public override AbstractNode VisitNegExpression([NotNull] VestaParser.NegExpressionContext context)
        {
            ExpressionNode result;

            result = new ExpressionNode(Enums.Operators.sub, null,null, (FactorNode)Visit(context.factor()));
            return result;
        }
        public override AbstractNode VisitMultiplicationExpression([NotNull] VestaParser.MultiplicationExpressionContext context)
        {
            ExpressionNode result;
            var expressionContexts = context.expression().ToArray();
            result = new ExpressionNode(Enums.StringToOperator(context.multOp().GetText()), (ExpressionNode)Visit(expressionContexts[0]),(ExpressionNode)Visit(expressionContexts[1]), null);
            return result;
        }
        public override AbstractNode VisitAdditionExpression([NotNull] VestaParser.AdditionExpressionContext context)
        {
            ExpressionNode result;
            var expressionContexts = context.expression().ToArray();
            result = new ExpressionNode(Enums.StringToOperator(context.addOp().GetText()), (ExpressionNode)Visit(expressionContexts[0]),(ExpressionNode)Visit(expressionContexts[1]), null);
            return result;
        }
        public override AbstractNode VisitCompareExpression([NotNull] VestaParser.CompareExpressionContext context)
        {
            ExpressionNode result;
            var expressionContexts = context.expression().ToArray();
            result = new ExpressionNode(Enums.StringToOperator(context.compareOp().GetText()), (ExpressionNode)Visit(expressionContexts[0]),(ExpressionNode)Visit(expressionContexts[1]), null);
            return result;
        }
        public override AbstractNode VisitBooleanExpression([NotNull] VestaParser.BooleanExpressionContext context)
        {
            ExpressionNode result;
            var expressionContexts = context.expression().ToArray();
            result = new ExpressionNode(Enums.StringToOperator(context.boolOp().GetText()), (ExpressionNode)Visit(expressionContexts[0]),(ExpressionNode)Visit(expressionContexts[1]), null);
            return result;
        }

        //Factor
        public override AbstractNode VisitParenthesizedExpression([NotNull] VestaParser.ParenthesizedExpressionContext context)
        {
            return new FactorNode((ExpressionNode)Visit(context.expression()), null, null, null, null, null, null);
        }
        public override AbstractNode VisitConstantExpression([NotNull] VestaParser.ConstantExpressionContext context)
        {
            FactorNode result;
            ConstantNode temp;
            var integer = context.constant().INTEGER();
            var boolean = context.constant().BOOL();
            if (integer != null) {
                temp = new ConstantNode(null, int.Parse(integer.GetText()));
            } else if (boolean != null) {
                temp = new ConstantNode(Convert.ToBoolean(boolean.GetText()), null);
            } else {
                throw new NotImplementedException();
            }
            result = new FactorNode(null, temp, null, null, null, null, null);
            return result;
        }
        public override AbstractNode VisitObjectExpression([NotNull] VestaParser.ObjectExpressionContext context)
        {
            FactorNode result = new FactorNode(null, null, (Factor2Node)Visit(context.factor2()), null, null, null, null);
            return result;
        }
        public override AbstractNode VisitArrayExpression([NotNull] VestaParser.ArrayExpressionContext context)
        {
            List<ExpressionNode> expressions = new List<ExpressionNode>();
            var expressionContexts = context.expression().ToArray();
            foreach (var expression in expressionContexts) {
                expressions.Add((ExpressionNode)Visit(expression));
            }
            return new FactorNode(null, null, null, new ArrayExpressionNode(expressions), null, null, null);
        }
        public override AbstractNode VisitMapExpression([NotNull] VestaParser.MapExpressionContext context)
        {
            return new FactorNode (null, null, null, null, new MapExpressionNode((ArrayDimensionsNode)Visit(context.arrayDimensions()), (MapLayerNode)Visit(context.mapLayer())), null, null);
        }
        public override AbstractNode VisitArrayAccess([NotNull] VestaParser.ArrayAccessContext context)
        {
            return new FactorNode (null, null, null, null, null, new ArrayAccessNode((Factor2Node)Visit(context.factor2()), (ArrayDimensionsNode)Visit(context.arrayDimensions())), null);
        }
        public override AbstractNode VisitMapAccess([NotNull] VestaParser.MapAccessContext context)
        {
            return new FactorNode (null, null, null, null, null, null, new MapAccessNode((Factor2Node)Visit(context.factor2()), context.IDENTIFIER().GetText(), (ArrayDimensionsNode)Visit(context.arrayDimensions())));
        }


        //Factor2
        public override AbstractNode VisitIdentifierAccess([NotNull] VestaParser.IdentifierAccessContext context)
        {
            Factor2Node result = new Factor2Node(context.IDENTIFIER().GetText(), null);
            return result;
        }
        public override AbstractNode VisitFunctionAccess([NotNull] VestaParser.FunctionAccessContext context)
        {
            Factor2Node result = new Factor2Node(null, (FunctionCallNode)Visit(context.functionCall()));
            return result;
        }
        public override AbstractNode VisitFunctionCall([NotNull] VestaParser.FunctionCallContext context)
        {
            FunctionCallNode result;
            VestaParser.ExpressionContext[] parameters = context.expression().ToArray();
            List<ExpressionNode> expParams = new List<ExpressionNode>();
            foreach (var parameter in parameters)
                expParams.Add((ExpressionNode)Visit(parameter));

            var Identifiers = context.IDENTIFIER().ToArray();
            if (Identifiers.Length == 2) {
                result = new FunctionCallNode(Identifiers[0].GetText(), Identifiers[1].GetText(), expParams);
            } else if (Identifiers.Length == 1) {
                result = new FunctionCallNode(null, Identifiers[0].GetText(), expParams);
            } else {
                throw new NotImplementedException();
            }
            return result;
        }


        public override AbstractNode VisitArrayDimensions([NotNull] VestaParser.ArrayDimensionsContext context)
        {
            VestaParser.ExpressionContext[] parameters = context.expression().ToArray();
            List<ExpressionNode> expParams = new List<ExpressionNode>();
            foreach (var parameter in parameters)
                expParams.Add((ExpressionNode)Visit(parameter));
            return new ArrayDimensionsNode(expParams);
        }
        public override AbstractNode VisitMapLayer([NotNull] VestaParser.MapLayerContext context)
        {
            List<IndividualLayerNode> individualLayers = ContextToNode<IndividualLayerNode>(context.individualLayer().ToArray());
            return new MapLayerNode(individualLayers);
        }
        public override AbstractNode VisitIndividualLayer([NotNull] VestaParser.IndividualLayerContext context)
        {
            return new IndividualLayerNode((TypeNode)Visit(context.identifierType()), context.IDENTIFIER().GetText(), context.expression() == null ? null : (ExpressionNode)Visit(context.expression()));
        }

        public override AbstractNode VisitBlock(VestaParser.BlockContext context)
        {
            List<StatementNode> statementNodes = ContextToNode<StatementNode>(context.statement().ToArray());
            BlockNode result = new BlockNode(statementNodes);
            return result;
        }


        private List<T> ContextToNode<T> (IEnumerable<Antlr4.Runtime.Tree.IParseTree> array) {
            List<T> nodes = new List<T>();
            foreach (var element in array)
                nodes.Add((T)Visit(element));
            return nodes;
        }
    }
}



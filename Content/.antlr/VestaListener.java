// Generated from /home/theisrm/Repo/School/AAUP4RoguelikeLang/Content/Vesta.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link VestaParser}.
 */
public interface VestaListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link VestaParser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(VestaParser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(VestaParser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#line}.
	 * @param ctx the parse tree
	 */
	void enterLine(VestaParser.LineContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#line}.
	 * @param ctx the parse tree
	 */
	void exitLine(VestaParser.LineContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterStatement(VestaParser.StatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitStatement(VestaParser.StatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#ifBlock}.
	 * @param ctx the parse tree
	 */
	void enterIfBlock(VestaParser.IfBlockContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#ifBlock}.
	 * @param ctx the parse tree
	 */
	void exitIfBlock(VestaParser.IfBlockContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#elseIfBlock}.
	 * @param ctx the parse tree
	 */
	void enterElseIfBlock(VestaParser.ElseIfBlockContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#elseIfBlock}.
	 * @param ctx the parse tree
	 */
	void exitElseIfBlock(VestaParser.ElseIfBlockContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#whileBlock}.
	 * @param ctx the parse tree
	 */
	void enterWhileBlock(VestaParser.WhileBlockContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#whileBlock}.
	 * @param ctx the parse tree
	 */
	void exitWhileBlock(VestaParser.WhileBlockContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#chanceBlock}.
	 * @param ctx the parse tree
	 */
	void enterChanceBlock(VestaParser.ChanceBlockContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#chanceBlock}.
	 * @param ctx the parse tree
	 */
	void exitChanceBlock(VestaParser.ChanceBlockContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#block}.
	 * @param ctx the parse tree
	 */
	void enterBlock(VestaParser.BlockContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#block}.
	 * @param ctx the parse tree
	 */
	void exitBlock(VestaParser.BlockContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#assignment}.
	 * @param ctx the parse tree
	 */
	void enterAssignment(VestaParser.AssignmentContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#assignment}.
	 * @param ctx the parse tree
	 */
	void exitAssignment(VestaParser.AssignmentContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#declartion}.
	 * @param ctx the parse tree
	 */
	void enterDeclartion(VestaParser.DeclartionContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#declartion}.
	 * @param ctx the parse tree
	 */
	void exitDeclartion(VestaParser.DeclartionContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#functionCall}.
	 * @param ctx the parse tree
	 */
	void enterFunctionCall(VestaParser.FunctionCallContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#functionCall}.
	 * @param ctx the parse tree
	 */
	void exitFunctionCall(VestaParser.FunctionCallContext ctx);
	/**
	 * Enter a parse tree produced by the {@code arrayIdentifierExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterArrayIdentifierExpression(VestaParser.ArrayIdentifierExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code arrayIdentifierExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitArrayIdentifierExpression(VestaParser.ArrayIdentifierExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code arrayOperationExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterArrayOperationExpression(VestaParser.ArrayOperationExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code arrayOperationExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitArrayOperationExpression(VestaParser.ArrayOperationExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code constantExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterConstantExpression(VestaParser.ConstantExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code constantExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitConstantExpression(VestaParser.ConstantExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code arrayExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterArrayExpression(VestaParser.ArrayExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code arrayExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitArrayExpression(VestaParser.ArrayExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code additionExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterAdditionExpression(VestaParser.AdditionExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code additionExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitAdditionExpression(VestaParser.AdditionExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code identifierExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterIdentifierExpression(VestaParser.IdentifierExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code identifierExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitIdentifierExpression(VestaParser.IdentifierExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code notExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterNotExpression(VestaParser.NotExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code notExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitNotExpression(VestaParser.NotExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code multiplicationExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterMultiplicationExpression(VestaParser.MultiplicationExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code multiplicationExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitMultiplicationExpression(VestaParser.MultiplicationExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code booleanExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterBooleanExpression(VestaParser.BooleanExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code booleanExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitBooleanExpression(VestaParser.BooleanExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code compareExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterCompareExpression(VestaParser.CompareExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code compareExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitCompareExpression(VestaParser.CompareExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code parenthesizedExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterParenthesizedExpression(VestaParser.ParenthesizedExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code parenthesizedExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitParenthesizedExpression(VestaParser.ParenthesizedExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code functionCallExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterFunctionCallExpression(VestaParser.FunctionCallExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code functionCallExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitFunctionCallExpression(VestaParser.FunctionCallExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code randomExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterRandomExpression(VestaParser.RandomExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code randomExpression}
	 * labeled alternative in {@link VestaParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitRandomExpression(VestaParser.RandomExpressionContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#arrayOp}.
	 * @param ctx the parse tree
	 */
	void enterArrayOp(VestaParser.ArrayOpContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#arrayOp}.
	 * @param ctx the parse tree
	 */
	void exitArrayOp(VestaParser.ArrayOpContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#multOp}.
	 * @param ctx the parse tree
	 */
	void enterMultOp(VestaParser.MultOpContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#multOp}.
	 * @param ctx the parse tree
	 */
	void exitMultOp(VestaParser.MultOpContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#addOp}.
	 * @param ctx the parse tree
	 */
	void enterAddOp(VestaParser.AddOpContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#addOp}.
	 * @param ctx the parse tree
	 */
	void exitAddOp(VestaParser.AddOpContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#compareOp}.
	 * @param ctx the parse tree
	 */
	void enterCompareOp(VestaParser.CompareOpContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#compareOp}.
	 * @param ctx the parse tree
	 */
	void exitCompareOp(VestaParser.CompareOpContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#boolOp}.
	 * @param ctx the parse tree
	 */
	void enterBoolOp(VestaParser.BoolOpContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#boolOp}.
	 * @param ctx the parse tree
	 */
	void exitBoolOp(VestaParser.BoolOpContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#constant}.
	 * @param ctx the parse tree
	 */
	void enterConstant(VestaParser.ConstantContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#constant}.
	 * @param ctx the parse tree
	 */
	void exitConstant(VestaParser.ConstantContext ctx);
	/**
	 * Enter a parse tree produced by {@link VestaParser#identifierType}.
	 * @param ctx the parse tree
	 */
	void enterIdentifierType(VestaParser.IdentifierTypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link VestaParser#identifierType}.
	 * @param ctx the parse tree
	 */
	void exitIdentifierType(VestaParser.IdentifierTypeContext ctx);
}
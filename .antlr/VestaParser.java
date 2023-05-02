// Generated from l:\Onedrive\Uddannelse\04-Aalborg universitet\4. semester\Project\AAUP4RoguelikeLang\Vesta.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class VestaParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, BOOL_OPERATOR=31, 
		INTEGER=32, BOOL=33, TYPE=34, IDENTIFIER=35, WS=36;
	public static final int
		RULE_program = 0, RULE_line = 1, RULE_statement = 2, RULE_ifBlock = 3, 
		RULE_elseIfBlock = 4, RULE_whileBlock = 5, RULE_chanceBlock = 6, RULE_block = 7, 
		RULE_assignment = 8, RULE_declaration = 9, RULE_functionCall = 10, RULE_expression = 11, 
		RULE_arithmeticExpression = 12, RULE_logicalExpression = 13, RULE_arrayOp = 14, 
		RULE_multOp = 15, RULE_addOp = 16, RULE_compareOp = 17, RULE_boolOp = 18, 
		RULE_identifierType = 19;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "line", "statement", "ifBlock", "elseIfBlock", "whileBlock", 
			"chanceBlock", "block", "assignment", "declaration", "functionCall", 
			"expression", "arithmeticExpression", "logicalExpression", "arrayOp", 
			"multOp", "addOp", "compareOp", "boolOp", "identifierType"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "';'", "'if'", "'('", "')'", "'else'", "'while'", "'chance'", "'{'", 
			"':'", "'}'", "'='", "'var'", "','", "'['", "']'", "'rand('", "'.'", 
			"'!'", "'remove'", "'add'", "'*'", "'/'", "'+'", "'-'", "'=='", "'!='", 
			"'>'", "'<'", "'>='", "'<='"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, "BOOL_OPERATOR", "INTEGER", 
			"BOOL", "TYPE", "IDENTIFIER", "WS"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "Vesta.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public VestaParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ProgramContext extends ParserRuleContext {
		public TerminalNode EOF() { return getToken(VestaParser.EOF, 0); }
		public List<LineContext> line() {
			return getRuleContexts(LineContext.class);
		}
		public LineContext line(int i) {
			return getRuleContext(LineContext.class,i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_program; }
	}

	public final ProgramContext program() throws RecognitionException {
		ProgramContext _localctx = new ProgramContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(43);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__5) | (1L << T__6) | (1L << T__11) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(40);
				line();
				}
				}
				setState(45);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(46);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LineContext extends ParserRuleContext {
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public IfBlockContext ifBlock() {
			return getRuleContext(IfBlockContext.class,0);
		}
		public WhileBlockContext whileBlock() {
			return getRuleContext(WhileBlockContext.class,0);
		}
		public ChanceBlockContext chanceBlock() {
			return getRuleContext(ChanceBlockContext.class,0);
		}
		public LineContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_line; }
	}

	public final LineContext line() throws RecognitionException {
		LineContext _localctx = new LineContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_line);
		try {
			setState(52);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__11:
			case IDENTIFIER:
				enterOuterAlt(_localctx, 1);
				{
				setState(48);
				statement();
				}
				break;
			case T__1:
				enterOuterAlt(_localctx, 2);
				{
				setState(49);
				ifBlock();
				}
				break;
			case T__5:
				enterOuterAlt(_localctx, 3);
				{
				setState(50);
				whileBlock();
				}
				break;
			case T__6:
				enterOuterAlt(_localctx, 4);
				{
				setState(51);
				chanceBlock();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StatementContext extends ParserRuleContext {
		public DeclarationContext declaration() {
			return getRuleContext(DeclarationContext.class,0);
		}
		public AssignmentContext assignment() {
			return getRuleContext(AssignmentContext.class,0);
		}
		public FunctionCallContext functionCall() {
			return getRuleContext(FunctionCallContext.class,0);
		}
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(57);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,2,_ctx) ) {
			case 1:
				{
				setState(54);
				declaration();
				}
				break;
			case 2:
				{
				setState(55);
				assignment();
				}
				break;
			case 3:
				{
				setState(56);
				functionCall();
				}
				break;
			}
			setState(59);
			match(T__0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IfBlockContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public BlockContext block() {
			return getRuleContext(BlockContext.class,0);
		}
		public ElseIfBlockContext elseIfBlock() {
			return getRuleContext(ElseIfBlockContext.class,0);
		}
		public IfBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_ifBlock; }
	}

	public final IfBlockContext ifBlock() throws RecognitionException {
		IfBlockContext _localctx = new IfBlockContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_ifBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(61);
			match(T__1);
			setState(62);
			match(T__2);
			setState(63);
			expression();
			setState(64);
			match(T__3);
			setState(65);
			block();
			setState(68);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__4) {
				{
				setState(66);
				match(T__4);
				setState(67);
				elseIfBlock();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ElseIfBlockContext extends ParserRuleContext {
		public BlockContext block() {
			return getRuleContext(BlockContext.class,0);
		}
		public IfBlockContext ifBlock() {
			return getRuleContext(IfBlockContext.class,0);
		}
		public ElseIfBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_elseIfBlock; }
	}

	public final ElseIfBlockContext elseIfBlock() throws RecognitionException {
		ElseIfBlockContext _localctx = new ElseIfBlockContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_elseIfBlock);
		try {
			setState(72);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__7:
				enterOuterAlt(_localctx, 1);
				{
				setState(70);
				block();
				}
				break;
			case T__1:
				enterOuterAlt(_localctx, 2);
				{
				setState(71);
				ifBlock();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class WhileBlockContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public BlockContext block() {
			return getRuleContext(BlockContext.class,0);
		}
		public WhileBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_whileBlock; }
	}

	public final WhileBlockContext whileBlock() throws RecognitionException {
		WhileBlockContext _localctx = new WhileBlockContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_whileBlock);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(74);
			match(T__5);
			setState(75);
			match(T__2);
			setState(76);
			expression();
			setState(77);
			match(T__3);
			setState(78);
			block();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ChanceBlockContext extends ParserRuleContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public List<BlockContext> block() {
			return getRuleContexts(BlockContext.class);
		}
		public BlockContext block(int i) {
			return getRuleContext(BlockContext.class,i);
		}
		public ChanceBlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_chanceBlock; }
	}

	public final ChanceBlockContext chanceBlock() throws RecognitionException {
		ChanceBlockContext _localctx = new ChanceBlockContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_chanceBlock);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(80);
			match(T__6);
			setState(81);
			match(T__7);
			setState(86); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(82);
				expression();
				setState(83);
				match(T__8);
				setState(84);
				block();
				}
				}
				setState(88); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__2) | (1L << T__7) | (1L << T__15) | (1L << T__17) | (1L << INTEGER) | (1L << BOOL) | (1L << IDENTIFIER))) != 0) );
			setState(90);
			match(T__9);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BlockContext extends ParserRuleContext {
		public List<LineContext> line() {
			return getRuleContexts(LineContext.class);
		}
		public LineContext line(int i) {
			return getRuleContext(LineContext.class,i);
		}
		public BlockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_block; }
	}

	public final BlockContext block() throws RecognitionException {
		BlockContext _localctx = new BlockContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_block);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(92);
			match(T__7);
			setState(96);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__5) | (1L << T__6) | (1L << T__11) | (1L << IDENTIFIER))) != 0)) {
				{
				{
				setState(93);
				line();
				}
				}
				setState(98);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(99);
			match(T__9);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AssignmentContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(VestaParser.IDENTIFIER, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public AssignmentContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignment; }
	}

	public final AssignmentContext assignment() throws RecognitionException {
		AssignmentContext _localctx = new AssignmentContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_assignment);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(101);
			match(IDENTIFIER);
			setState(102);
			match(T__10);
			setState(103);
			expression();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DeclarationContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(VestaParser.IDENTIFIER, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public DeclarationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_declaration; }
	}

	public final DeclarationContext declaration() throws RecognitionException {
		DeclarationContext _localctx = new DeclarationContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_declaration);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(105);
			match(T__11);
			setState(106);
			match(IDENTIFIER);
			setState(107);
			match(T__10);
			setState(108);
			expression();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class FunctionCallContext extends ParserRuleContext {
		public TerminalNode IDENTIFIER() { return getToken(VestaParser.IDENTIFIER, 0); }
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public FunctionCallContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_functionCall; }
	}

	public final FunctionCallContext functionCall() throws RecognitionException {
		FunctionCallContext _localctx = new FunctionCallContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_functionCall);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(110);
			match(IDENTIFIER);
			setState(111);
			match(T__2);
			setState(120);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__2) | (1L << T__7) | (1L << T__15) | (1L << T__17) | (1L << INTEGER) | (1L << BOOL) | (1L << IDENTIFIER))) != 0)) {
				{
				setState(112);
				expression();
				setState(117);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==T__12) {
					{
					{
					setState(113);
					match(T__12);
					setState(114);
					expression();
					}
					}
					setState(119);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
			}

			setState(122);
			match(T__3);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public LogicalExpressionContext logicalExpression() {
			return getRuleContext(LogicalExpressionContext.class,0);
		}
		public ArithmeticExpressionContext arithmeticExpression() {
			return getRuleContext(ArithmeticExpressionContext.class,0);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	}

	public final ExpressionContext expression() throws RecognitionException {
		ExpressionContext _localctx = new ExpressionContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_expression);
		try {
			setState(130);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,9,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(124);
				match(T__2);
				setState(125);
				expression();
				setState(126);
				match(T__3);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(128);
				logicalExpression(0);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(129);
				arithmeticExpression(0);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArithmeticExpressionContext extends ParserRuleContext {
		public ArithmeticExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arithmeticExpression; }
	 
		public ArithmeticExpressionContext() { }
		public void copyFrom(ArithmeticExpressionContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class ArrayIdentifierArithmeticExpressionContext extends ArithmeticExpressionContext {
		public TerminalNode IDENTIFIER() { return getToken(VestaParser.IDENTIFIER, 0); }
		public List<ArithmeticExpressionContext> arithmeticExpression() {
			return getRuleContexts(ArithmeticExpressionContext.class);
		}
		public ArithmeticExpressionContext arithmeticExpression(int i) {
			return getRuleContext(ArithmeticExpressionContext.class,i);
		}
		public ArrayIdentifierArithmeticExpressionContext(ArithmeticExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ArrayArithmeticExpressionContext extends ArithmeticExpressionContext {
		public List<ArithmeticExpressionContext> arithmeticExpression() {
			return getRuleContexts(ArithmeticExpressionContext.class);
		}
		public ArithmeticExpressionContext arithmeticExpression(int i) {
			return getRuleContext(ArithmeticExpressionContext.class,i);
		}
		public ArrayArithmeticExpressionContext(ArithmeticExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ConstantArithmeticExpressionContext extends ArithmeticExpressionContext {
		public TerminalNode INTEGER() { return getToken(VestaParser.INTEGER, 0); }
		public ConstantArithmeticExpressionContext(ArithmeticExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class FunctionCallArithmeticExpressionContext extends ArithmeticExpressionContext {
		public FunctionCallContext functionCall() {
			return getRuleContext(FunctionCallContext.class,0);
		}
		public FunctionCallArithmeticExpressionContext(ArithmeticExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ArrayOperationArithmeticExpressionContext extends ArithmeticExpressionContext {
		public TerminalNode IDENTIFIER() { return getToken(VestaParser.IDENTIFIER, 0); }
		public ArrayOpContext arrayOp() {
			return getRuleContext(ArrayOpContext.class,0);
		}
		public ArithmeticExpressionContext arithmeticExpression() {
			return getRuleContext(ArithmeticExpressionContext.class,0);
		}
		public ArrayOperationArithmeticExpressionContext(ArithmeticExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class IdentifierArithmeticExpressionContext extends ArithmeticExpressionContext {
		public TerminalNode IDENTIFIER() { return getToken(VestaParser.IDENTIFIER, 0); }
		public IdentifierArithmeticExpressionContext(ArithmeticExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class MultiplicationArithmeticExpressionContext extends ArithmeticExpressionContext {
		public List<ArithmeticExpressionContext> arithmeticExpression() {
			return getRuleContexts(ArithmeticExpressionContext.class);
		}
		public ArithmeticExpressionContext arithmeticExpression(int i) {
			return getRuleContext(ArithmeticExpressionContext.class,i);
		}
		public MultOpContext multOp() {
			return getRuleContext(MultOpContext.class,0);
		}
		public MultiplicationArithmeticExpressionContext(ArithmeticExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class AdditionArithmeticExpressionContext extends ArithmeticExpressionContext {
		public List<ArithmeticExpressionContext> arithmeticExpression() {
			return getRuleContexts(ArithmeticExpressionContext.class);
		}
		public ArithmeticExpressionContext arithmeticExpression(int i) {
			return getRuleContext(ArithmeticExpressionContext.class,i);
		}
		public AddOpContext addOp() {
			return getRuleContext(AddOpContext.class,0);
		}
		public AdditionArithmeticExpressionContext(ArithmeticExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class RandomArithmeticExpressionContext extends ArithmeticExpressionContext {
		public List<ArithmeticExpressionContext> arithmeticExpression() {
			return getRuleContexts(ArithmeticExpressionContext.class);
		}
		public ArithmeticExpressionContext arithmeticExpression(int i) {
			return getRuleContext(ArithmeticExpressionContext.class,i);
		}
		public RandomArithmeticExpressionContext(ArithmeticExpressionContext ctx) { copyFrom(ctx); }
	}

	public final ArithmeticExpressionContext arithmeticExpression() throws RecognitionException {
		return arithmeticExpression(0);
	}

	private ArithmeticExpressionContext arithmeticExpression(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ArithmeticExpressionContext _localctx = new ArithmeticExpressionContext(_ctx, _parentState);
		ArithmeticExpressionContext _prevctx = _localctx;
		int _startState = 24;
		enterRecursionRule(_localctx, 24, RULE_arithmeticExpression, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(169);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,12,_ctx) ) {
			case 1:
				{
				_localctx = new ConstantArithmeticExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(133);
				match(INTEGER);
				}
				break;
			case 2:
				{
				_localctx = new IdentifierArithmeticExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(134);
				match(IDENTIFIER);
				}
				break;
			case 3:
				{
				_localctx = new ArrayIdentifierArithmeticExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(135);
				match(IDENTIFIER);
				setState(140); 
				_errHandler.sync(this);
				_alt = 1;
				do {
					switch (_alt) {
					case 1:
						{
						{
						setState(136);
						match(T__13);
						setState(137);
						arithmeticExpression(0);
						setState(138);
						match(T__14);
						}
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					setState(142); 
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,10,_ctx);
				} while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER );
				}
				break;
			case 4:
				{
				_localctx = new ArrayArithmeticExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(144);
				match(T__7);
				{
				setState(145);
				arithmeticExpression(0);
				setState(150);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==T__12) {
					{
					{
					setState(146);
					match(T__12);
					setState(147);
					arithmeticExpression(0);
					}
					}
					setState(152);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				setState(153);
				match(T__9);
				}
				break;
			case 5:
				{
				_localctx = new RandomArithmeticExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(155);
				match(T__15);
				setState(156);
				arithmeticExpression(0);
				setState(157);
				match(T__12);
				setState(158);
				arithmeticExpression(0);
				setState(159);
				match(T__3);
				}
				break;
			case 6:
				{
				_localctx = new FunctionCallArithmeticExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(161);
				functionCall();
				}
				break;
			case 7:
				{
				_localctx = new ArrayOperationArithmeticExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(162);
				match(IDENTIFIER);
				setState(163);
				match(T__16);
				setState(164);
				arrayOp();
				setState(165);
				match(T__2);
				setState(166);
				arithmeticExpression(0);
				setState(167);
				match(T__3);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(181);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,14,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(179);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,13,_ctx) ) {
					case 1:
						{
						_localctx = new MultiplicationArithmeticExpressionContext(new ArithmeticExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_arithmeticExpression);
						setState(171);
						if (!(precpred(_ctx, 2))) throw new FailedPredicateException(this, "precpred(_ctx, 2)");
						setState(172);
						multOp();
						setState(173);
						arithmeticExpression(3);
						}
						break;
					case 2:
						{
						_localctx = new AdditionArithmeticExpressionContext(new ArithmeticExpressionContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_arithmeticExpression);
						setState(175);
						if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
						setState(176);
						addOp();
						setState(177);
						arithmeticExpression(2);
						}
						break;
					}
					} 
				}
				setState(183);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,14,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class LogicalExpressionContext extends ParserRuleContext {
		public LogicalExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_logicalExpression; }
	 
		public LogicalExpressionContext() { }
		public void copyFrom(LogicalExpressionContext ctx) {
			super.copyFrom(ctx);
		}
	}
	public static class ArrayOperationLogicalExpressionContext extends LogicalExpressionContext {
		public TerminalNode IDENTIFIER() { return getToken(VestaParser.IDENTIFIER, 0); }
		public ArrayOpContext arrayOp() {
			return getRuleContext(ArrayOpContext.class,0);
		}
		public LogicalExpressionContext logicalExpression() {
			return getRuleContext(LogicalExpressionContext.class,0);
		}
		public ArrayOperationLogicalExpressionContext(LogicalExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ConstantLogicalExpressionContext extends LogicalExpressionContext {
		public TerminalNode BOOL() { return getToken(VestaParser.BOOL, 0); }
		public ConstantLogicalExpressionContext(LogicalExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class CompareLogicalExpressionContext extends LogicalExpressionContext {
		public List<ArithmeticExpressionContext> arithmeticExpression() {
			return getRuleContexts(ArithmeticExpressionContext.class);
		}
		public ArithmeticExpressionContext arithmeticExpression(int i) {
			return getRuleContext(ArithmeticExpressionContext.class,i);
		}
		public CompareOpContext compareOp() {
			return getRuleContext(CompareOpContext.class,0);
		}
		public CompareLogicalExpressionContext(LogicalExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ArrayLogicalExpressionContext extends LogicalExpressionContext {
		public List<LogicalExpressionContext> logicalExpression() {
			return getRuleContexts(LogicalExpressionContext.class);
		}
		public LogicalExpressionContext logicalExpression(int i) {
			return getRuleContext(LogicalExpressionContext.class,i);
		}
		public ArrayLogicalExpressionContext(LogicalExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class ArrayIdentifierLogicalExpressionContext extends LogicalExpressionContext {
		public TerminalNode IDENTIFIER() { return getToken(VestaParser.IDENTIFIER, 0); }
		public List<ArithmeticExpressionContext> arithmeticExpression() {
			return getRuleContexts(ArithmeticExpressionContext.class);
		}
		public ArithmeticExpressionContext arithmeticExpression(int i) {
			return getRuleContext(ArithmeticExpressionContext.class,i);
		}
		public ArrayIdentifierLogicalExpressionContext(LogicalExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class FunctionCallLogicalExpressionContext extends LogicalExpressionContext {
		public FunctionCallContext functionCall() {
			return getRuleContext(FunctionCallContext.class,0);
		}
		public FunctionCallLogicalExpressionContext(LogicalExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class IdentifierLogicalExpressionContext extends LogicalExpressionContext {
		public TerminalNode IDENTIFIER() { return getToken(VestaParser.IDENTIFIER, 0); }
		public IdentifierLogicalExpressionContext(LogicalExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class NotLogicalExpressionContext extends LogicalExpressionContext {
		public LogicalExpressionContext logicalExpression() {
			return getRuleContext(LogicalExpressionContext.class,0);
		}
		public NotLogicalExpressionContext(LogicalExpressionContext ctx) { copyFrom(ctx); }
	}
	public static class BooleanLogicalExpressionContext extends LogicalExpressionContext {
		public List<LogicalExpressionContext> logicalExpression() {
			return getRuleContexts(LogicalExpressionContext.class);
		}
		public LogicalExpressionContext logicalExpression(int i) {
			return getRuleContext(LogicalExpressionContext.class,i);
		}
		public BoolOpContext boolOp() {
			return getRuleContext(BoolOpContext.class,0);
		}
		public BooleanLogicalExpressionContext(LogicalExpressionContext ctx) { copyFrom(ctx); }
	}

	public final LogicalExpressionContext logicalExpression() throws RecognitionException {
		return logicalExpression(0);
	}

	private LogicalExpressionContext logicalExpression(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		LogicalExpressionContext _localctx = new LogicalExpressionContext(_ctx, _parentState);
		LogicalExpressionContext _prevctx = _localctx;
		int _startState = 26;
		enterRecursionRule(_localctx, 26, RULE_logicalExpression, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(221);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,17,_ctx) ) {
			case 1:
				{
				_localctx = new ConstantLogicalExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(185);
				match(BOOL);
				}
				break;
			case 2:
				{
				_localctx = new IdentifierLogicalExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(186);
				match(IDENTIFIER);
				}
				break;
			case 3:
				{
				_localctx = new ArrayIdentifierLogicalExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(187);
				match(IDENTIFIER);
				setState(192); 
				_errHandler.sync(this);
				_alt = 1;
				do {
					switch (_alt) {
					case 1:
						{
						{
						setState(188);
						match(T__13);
						setState(189);
						arithmeticExpression(0);
						setState(190);
						match(T__14);
						}
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					setState(194); 
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,15,_ctx);
				} while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER );
				}
				break;
			case 4:
				{
				_localctx = new ArrayLogicalExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(196);
				match(T__7);
				{
				setState(197);
				logicalExpression(0);
				setState(202);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==T__12) {
					{
					{
					setState(198);
					match(T__12);
					setState(199);
					logicalExpression(0);
					}
					}
					setState(204);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				setState(205);
				match(T__9);
				}
				break;
			case 5:
				{
				_localctx = new FunctionCallLogicalExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(207);
				functionCall();
				}
				break;
			case 6:
				{
				_localctx = new ArrayOperationLogicalExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(208);
				match(IDENTIFIER);
				setState(209);
				match(T__16);
				setState(210);
				arrayOp();
				setState(211);
				match(T__2);
				setState(212);
				logicalExpression(0);
				setState(213);
				match(T__3);
				}
				break;
			case 7:
				{
				_localctx = new NotLogicalExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(215);
				match(T__17);
				setState(216);
				logicalExpression(3);
				}
				break;
			case 8:
				{
				_localctx = new CompareLogicalExpressionContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(217);
				arithmeticExpression(0);
				setState(218);
				compareOp();
				setState(219);
				arithmeticExpression(0);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(229);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,18,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					{
					_localctx = new BooleanLogicalExpressionContext(new LogicalExpressionContext(_parentctx, _parentState));
					pushNewRecursionContext(_localctx, _startState, RULE_logicalExpression);
					setState(223);
					if (!(precpred(_ctx, 1))) throw new FailedPredicateException(this, "precpred(_ctx, 1)");
					setState(224);
					boolOp();
					setState(225);
					logicalExpression(2);
					}
					} 
				}
				setState(231);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,18,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class ArrayOpContext extends ParserRuleContext {
		public ArrayOpContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_arrayOp; }
	}

	public final ArrayOpContext arrayOp() throws RecognitionException {
		ArrayOpContext _localctx = new ArrayOpContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_arrayOp);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(232);
			_la = _input.LA(1);
			if ( !(_la==T__18 || _la==T__19) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class MultOpContext extends ParserRuleContext {
		public MultOpContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_multOp; }
	}

	public final MultOpContext multOp() throws RecognitionException {
		MultOpContext _localctx = new MultOpContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_multOp);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(234);
			_la = _input.LA(1);
			if ( !(_la==T__20 || _la==T__21) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AddOpContext extends ParserRuleContext {
		public AddOpContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_addOp; }
	}

	public final AddOpContext addOp() throws RecognitionException {
		AddOpContext _localctx = new AddOpContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_addOp);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(236);
			_la = _input.LA(1);
			if ( !(_la==T__22 || _la==T__23) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class CompareOpContext extends ParserRuleContext {
		public CompareOpContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_compareOp; }
	}

	public final CompareOpContext compareOp() throws RecognitionException {
		CompareOpContext _localctx = new CompareOpContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_compareOp);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(238);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__24) | (1L << T__25) | (1L << T__26) | (1L << T__27) | (1L << T__28) | (1L << T__29))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class BoolOpContext extends ParserRuleContext {
		public TerminalNode BOOL_OPERATOR() { return getToken(VestaParser.BOOL_OPERATOR, 0); }
		public BoolOpContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_boolOp; }
	}

	public final BoolOpContext boolOp() throws RecognitionException {
		BoolOpContext _localctx = new BoolOpContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_boolOp);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(240);
			match(BOOL_OPERATOR);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IdentifierTypeContext extends ParserRuleContext {
		public TerminalNode TYPE() { return getToken(VestaParser.TYPE, 0); }
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public IdentifierTypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_identifierType; }
	}

	public final IdentifierTypeContext identifierType() throws RecognitionException {
		IdentifierTypeContext _localctx = new IdentifierTypeContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_identifierType);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(242);
			match(TYPE);
			setState(249);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__13) {
				{
				{
				setState(243);
				match(T__13);
				setState(244);
				expression();
				setState(245);
				match(T__14);
				}
				}
				setState(251);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 12:
			return arithmeticExpression_sempred((ArithmeticExpressionContext)_localctx, predIndex);
		case 13:
			return logicalExpression_sempred((LogicalExpressionContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean arithmeticExpression_sempred(ArithmeticExpressionContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 2);
		case 1:
			return precpred(_ctx, 1);
		}
		return true;
	}
	private boolean logicalExpression_sempred(LogicalExpressionContext _localctx, int predIndex) {
		switch (predIndex) {
		case 2:
			return precpred(_ctx, 1);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3&\u00ff\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\3\2\7\2,\n\2\f\2\16\2/\13\2\3\2\3\2\3\3"+
		"\3\3\3\3\3\3\5\3\67\n\3\3\4\3\4\3\4\5\4<\n\4\3\4\3\4\3\5\3\5\3\5\3\5\3"+
		"\5\3\5\3\5\5\5G\n\5\3\6\3\6\5\6K\n\6\3\7\3\7\3\7\3\7\3\7\3\7\3\b\3\b\3"+
		"\b\3\b\3\b\3\b\6\bY\n\b\r\b\16\bZ\3\b\3\b\3\t\3\t\7\ta\n\t\f\t\16\td\13"+
		"\t\3\t\3\t\3\n\3\n\3\n\3\n\3\13\3\13\3\13\3\13\3\13\3\f\3\f\3\f\3\f\3"+
		"\f\7\fv\n\f\f\f\16\fy\13\f\5\f{\n\f\3\f\3\f\3\r\3\r\3\r\3\r\3\r\3\r\5"+
		"\r\u0085\n\r\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\6\16\u008f\n\16\r"+
		"\16\16\16\u0090\3\16\3\16\3\16\3\16\7\16\u0097\n\16\f\16\16\16\u009a\13"+
		"\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3"+
		"\16\3\16\3\16\5\16\u00ac\n\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16\3\16"+
		"\7\16\u00b6\n\16\f\16\16\16\u00b9\13\16\3\17\3\17\3\17\3\17\3\17\3\17"+
		"\3\17\3\17\6\17\u00c3\n\17\r\17\16\17\u00c4\3\17\3\17\3\17\3\17\7\17\u00cb"+
		"\n\17\f\17\16\17\u00ce\13\17\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3\17\3"+
		"\17\3\17\3\17\3\17\3\17\3\17\3\17\3\17\5\17\u00e0\n\17\3\17\3\17\3\17"+
		"\3\17\7\17\u00e6\n\17\f\17\16\17\u00e9\13\17\3\20\3\20\3\21\3\21\3\22"+
		"\3\22\3\23\3\23\3\24\3\24\3\25\3\25\3\25\3\25\3\25\7\25\u00fa\n\25\f\25"+
		"\16\25\u00fd\13\25\3\25\2\4\32\34\26\2\4\6\b\n\f\16\20\22\24\26\30\32"+
		"\34\36 \"$&(\2\6\3\2\25\26\3\2\27\30\3\2\31\32\3\2\33 \2\u010d\2-\3\2"+
		"\2\2\4\66\3\2\2\2\6;\3\2\2\2\b?\3\2\2\2\nJ\3\2\2\2\fL\3\2\2\2\16R\3\2"+
		"\2\2\20^\3\2\2\2\22g\3\2\2\2\24k\3\2\2\2\26p\3\2\2\2\30\u0084\3\2\2\2"+
		"\32\u00ab\3\2\2\2\34\u00df\3\2\2\2\36\u00ea\3\2\2\2 \u00ec\3\2\2\2\"\u00ee"+
		"\3\2\2\2$\u00f0\3\2\2\2&\u00f2\3\2\2\2(\u00f4\3\2\2\2*,\5\4\3\2+*\3\2"+
		"\2\2,/\3\2\2\2-+\3\2\2\2-.\3\2\2\2.\60\3\2\2\2/-\3\2\2\2\60\61\7\2\2\3"+
		"\61\3\3\2\2\2\62\67\5\6\4\2\63\67\5\b\5\2\64\67\5\f\7\2\65\67\5\16\b\2"+
		"\66\62\3\2\2\2\66\63\3\2\2\2\66\64\3\2\2\2\66\65\3\2\2\2\67\5\3\2\2\2"+
		"8<\5\24\13\29<\5\22\n\2:<\5\26\f\2;8\3\2\2\2;9\3\2\2\2;:\3\2\2\2<=\3\2"+
		"\2\2=>\7\3\2\2>\7\3\2\2\2?@\7\4\2\2@A\7\5\2\2AB\5\30\r\2BC\7\6\2\2CF\5"+
		"\20\t\2DE\7\7\2\2EG\5\n\6\2FD\3\2\2\2FG\3\2\2\2G\t\3\2\2\2HK\5\20\t\2"+
		"IK\5\b\5\2JH\3\2\2\2JI\3\2\2\2K\13\3\2\2\2LM\7\b\2\2MN\7\5\2\2NO\5\30"+
		"\r\2OP\7\6\2\2PQ\5\20\t\2Q\r\3\2\2\2RS\7\t\2\2SX\7\n\2\2TU\5\30\r\2UV"+
		"\7\13\2\2VW\5\20\t\2WY\3\2\2\2XT\3\2\2\2YZ\3\2\2\2ZX\3\2\2\2Z[\3\2\2\2"+
		"[\\\3\2\2\2\\]\7\f\2\2]\17\3\2\2\2^b\7\n\2\2_a\5\4\3\2`_\3\2\2\2ad\3\2"+
		"\2\2b`\3\2\2\2bc\3\2\2\2ce\3\2\2\2db\3\2\2\2ef\7\f\2\2f\21\3\2\2\2gh\7"+
		"%\2\2hi\7\r\2\2ij\5\30\r\2j\23\3\2\2\2kl\7\16\2\2lm\7%\2\2mn\7\r\2\2n"+
		"o\5\30\r\2o\25\3\2\2\2pq\7%\2\2qz\7\5\2\2rw\5\30\r\2st\7\17\2\2tv\5\30"+
		"\r\2us\3\2\2\2vy\3\2\2\2wu\3\2\2\2wx\3\2\2\2x{\3\2\2\2yw\3\2\2\2zr\3\2"+
		"\2\2z{\3\2\2\2{|\3\2\2\2|}\7\6\2\2}\27\3\2\2\2~\177\7\5\2\2\177\u0080"+
		"\5\30\r\2\u0080\u0081\7\6\2\2\u0081\u0085\3\2\2\2\u0082\u0085\5\34\17"+
		"\2\u0083\u0085\5\32\16\2\u0084~\3\2\2\2\u0084\u0082\3\2\2\2\u0084\u0083"+
		"\3\2\2\2\u0085\31\3\2\2\2\u0086\u0087\b\16\1\2\u0087\u00ac\7\"\2\2\u0088"+
		"\u00ac\7%\2\2\u0089\u008e\7%\2\2\u008a\u008b\7\20\2\2\u008b\u008c\5\32"+
		"\16\2\u008c\u008d\7\21\2\2\u008d\u008f\3\2\2\2\u008e\u008a\3\2\2\2\u008f"+
		"\u0090\3\2\2\2\u0090\u008e\3\2\2\2\u0090\u0091\3\2\2\2\u0091\u00ac\3\2"+
		"\2\2\u0092\u0093\7\n\2\2\u0093\u0098\5\32\16\2\u0094\u0095\7\17\2\2\u0095"+
		"\u0097\5\32\16\2\u0096\u0094\3\2\2\2\u0097\u009a\3\2\2\2\u0098\u0096\3"+
		"\2\2\2\u0098\u0099\3\2\2\2\u0099\u009b\3\2\2\2\u009a\u0098\3\2\2\2\u009b"+
		"\u009c\7\f\2\2\u009c\u00ac\3\2\2\2\u009d\u009e\7\22\2\2\u009e\u009f\5"+
		"\32\16\2\u009f\u00a0\7\17\2\2\u00a0\u00a1\5\32\16\2\u00a1\u00a2\7\6\2"+
		"\2\u00a2\u00ac\3\2\2\2\u00a3\u00ac\5\26\f\2\u00a4\u00a5\7%\2\2\u00a5\u00a6"+
		"\7\23\2\2\u00a6\u00a7\5\36\20\2\u00a7\u00a8\7\5\2\2\u00a8\u00a9\5\32\16"+
		"\2\u00a9\u00aa\7\6\2\2\u00aa\u00ac\3\2\2\2\u00ab\u0086\3\2\2\2\u00ab\u0088"+
		"\3\2\2\2\u00ab\u0089\3\2\2\2\u00ab\u0092\3\2\2\2\u00ab\u009d\3\2\2\2\u00ab"+
		"\u00a3\3\2\2\2\u00ab\u00a4\3\2\2\2\u00ac\u00b7\3\2\2\2\u00ad\u00ae\f\4"+
		"\2\2\u00ae\u00af\5 \21\2\u00af\u00b0\5\32\16\5\u00b0\u00b6\3\2\2\2\u00b1"+
		"\u00b2\f\3\2\2\u00b2\u00b3\5\"\22\2\u00b3\u00b4\5\32\16\4\u00b4\u00b6"+
		"\3\2\2\2\u00b5\u00ad\3\2\2\2\u00b5\u00b1\3\2\2\2\u00b6\u00b9\3\2\2\2\u00b7"+
		"\u00b5\3\2\2\2\u00b7\u00b8\3\2\2\2\u00b8\33\3\2\2\2\u00b9\u00b7\3\2\2"+
		"\2\u00ba\u00bb\b\17\1\2\u00bb\u00e0\7#\2\2\u00bc\u00e0\7%\2\2\u00bd\u00c2"+
		"\7%\2\2\u00be\u00bf\7\20\2\2\u00bf\u00c0\5\32\16\2\u00c0\u00c1\7\21\2"+
		"\2\u00c1\u00c3\3\2\2\2\u00c2\u00be\3\2\2\2\u00c3\u00c4\3\2\2\2\u00c4\u00c2"+
		"\3\2\2\2\u00c4\u00c5\3\2\2\2\u00c5\u00e0\3\2\2\2\u00c6\u00c7\7\n\2\2\u00c7"+
		"\u00cc\5\34\17\2\u00c8\u00c9\7\17\2\2\u00c9\u00cb\5\34\17\2\u00ca\u00c8"+
		"\3\2\2\2\u00cb\u00ce\3\2\2\2\u00cc\u00ca\3\2\2\2\u00cc\u00cd\3\2\2\2\u00cd"+
		"\u00cf\3\2\2\2\u00ce\u00cc\3\2\2\2\u00cf\u00d0\7\f\2\2\u00d0\u00e0\3\2"+
		"\2\2\u00d1\u00e0\5\26\f\2\u00d2\u00d3\7%\2\2\u00d3\u00d4\7\23\2\2\u00d4"+
		"\u00d5\5\36\20\2\u00d5\u00d6\7\5\2\2\u00d6\u00d7\5\34\17\2\u00d7\u00d8"+
		"\7\6\2\2\u00d8\u00e0\3\2\2\2\u00d9\u00da\7\24\2\2\u00da\u00e0\5\34\17"+
		"\5\u00db\u00dc\5\32\16\2\u00dc\u00dd\5$\23\2\u00dd\u00de\5\32\16\2\u00de"+
		"\u00e0\3\2\2\2\u00df\u00ba\3\2\2\2\u00df\u00bc\3\2\2\2\u00df\u00bd\3\2"+
		"\2\2\u00df\u00c6\3\2\2\2\u00df\u00d1\3\2\2\2\u00df\u00d2\3\2\2\2\u00df"+
		"\u00d9\3\2\2\2\u00df\u00db\3\2\2\2\u00e0\u00e7\3\2\2\2\u00e1\u00e2\f\3"+
		"\2\2\u00e2\u00e3\5&\24\2\u00e3\u00e4\5\34\17\4\u00e4\u00e6\3\2\2\2\u00e5"+
		"\u00e1\3\2\2\2\u00e6\u00e9\3\2\2\2\u00e7\u00e5\3\2\2\2\u00e7\u00e8\3\2"+
		"\2\2\u00e8\35\3\2\2\2\u00e9\u00e7\3\2\2\2\u00ea\u00eb\t\2\2\2\u00eb\37"+
		"\3\2\2\2\u00ec\u00ed\t\3\2\2\u00ed!\3\2\2\2\u00ee\u00ef\t\4\2\2\u00ef"+
		"#\3\2\2\2\u00f0\u00f1\t\5\2\2\u00f1%\3\2\2\2\u00f2\u00f3\7!\2\2\u00f3"+
		"\'\3\2\2\2\u00f4\u00fb\7$\2\2\u00f5\u00f6\7\20\2\2\u00f6\u00f7\5\30\r"+
		"\2\u00f7\u00f8\7\21\2\2\u00f8\u00fa\3\2\2\2\u00f9\u00f5\3\2\2\2\u00fa"+
		"\u00fd\3\2\2\2\u00fb\u00f9\3\2\2\2\u00fb\u00fc\3\2\2\2\u00fc)\3\2\2\2"+
		"\u00fd\u00fb\3\2\2\2\26-\66;FJZbwz\u0084\u0090\u0098\u00ab\u00b5\u00b7"+
		"\u00c4\u00cc\u00df\u00e7\u00fb";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}
grammar Vesta;

program: library* line* EOF;

library: 'using' IDENTIFIER ';';

line: statement | functionDecl;

statement : (varDecl | assignment | expression) ';' | block | ifStatement | whileStatement | forStatement | chance ;

ifStatement: 'if' '(' expression')' block ('else' block)?;

whileStatement: 'while' '(' expression ')' block;

forStatement: 'for' '(' varDecl ';'  expression ';' assignment ')' block;

chance: 'chance' '{' (expression ':' block )+ '}';

block: '{' statement* '}';

varDecl
   : identifierType IDENTIFIER        #varDeclaration
   | allType assignment        #varInitialization
   ;


functionDecl: parameterType IDENTIFIER '('(funcParams)?')' funcBody;

funcParams: parameter (',' parameter)* ;
parameter: parameterType IDENTIFIER;
parameterType: (TYPE | COMPLEXTYPE) (parameterArr)? ;
parameterArr: '[' (paramaterArrayDenoter)* ']';
paramaterArrayDenoter: ',';

funcBody: '{' statement*  returnStmt '}'; //Can we put return stmt up in statement?
returnStmt:  'return ' expression ';';

expression
   : factor                             #factorExpression
   | '!' factor						#notExpression
   | '-' factor 			 			#negExpression
   | expression multOp expression          		#multiplicationExpression
   | expression addOp expression          	 	#additionExpression
   | expression compareOp expression   	  	#compareExpression
   | expression boolOp expression 			#booleanExpression
;
factor
   : '(' expression ')'                             #parenthesizedExpression
   | constant                                       #constantExpression
   | factor2                                        #objectExpression
   | '{' (expression(','expression)*) '}'				#arrayExpression
   | arrayDimensions mapLayer 				#mapExpression
   | factor2 arrayDimensions					#arrayAccess
   | factor2 '.' IDENTIFIER (arrayDimensions)?			#mapAccess
;
factor2
	: IDENTIFIER                        #identifierAccess
  	| functionCall 						#functionAccess
	;

arrayDimensions: '[' expression (',' expression )* ']' ;
mapLayer: '{' individualLayer (';' individualLayer)* '}' ;
individualLayer: identifierType IDENTIFIER ('=' expression)? ;


assignment: IDENTIFIER (arrayDimensions)? '=' expression | mapAssignment;
mapAssignment: IDENTIFIER '.' IDENTIFIER (arrayDimensions)? '=' expression;
   

functionCall: (IDENTIFIER'.')? IDENTIFIER '(' (expression (',' expression)*)? ')';

allType: COMPLEXTYPE | identifierType;

identifierType: TYPE (arrayDimensions)? ;

multOp: '*' | '/';
addOp: '+' | '-';
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=';

boolOp: '&&' | '||' ;

constant: INTEGER | BOOL;

TYPE: 'int' | 'bool' ;
COMPLEXTYPE: 'map';

INTEGER:  [0-9]+ ;
BOOL: 'true' | 'false';

IDENTIFIER: [a-zA-Z] [a-zA-Z0-9]*;

WS: [ \t\r\n]+ -> skip;

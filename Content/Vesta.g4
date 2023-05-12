grammar Vesta;

program: library* line* EOF;

library: 'using' IDENTIFIER ';';

line: statement | functionDecl;

statement : (varDecl | assignment | expression | returnStmt) ';' | block | ifStatement | whileStatement | forStatement | chance ;

ifStatement: 'if' '(' expression ')' block ('else' elseIfBlock)?;

elseIfBlock: block | ifStatement;

whileStatement: 'while' '(' expression ')' block;

forStatement: 'for' '(' varDecl ';'  expression ';' assignment ')' block;

chance: 'chance' '{' (expression ':' block )+ '}';

block: '{' statement* '}';

//varDecl
//   : identifierType IDENTIFIER        #varDeclaration
//   | identifierType assignment        #varInitialization
//   | 'map' IDENTIFIER '=' arrayDimensions mapLayer #mapDeclaration
//   ;

varDecl
   : identifierType IDENTIFIER        #varDeclaration
   | allType assignment        #varInitialization
   ;

functionDecl: allType IDENTIFIER '('(funcParams)?')' block;

funcParams: parameter (',' parameter)* ;
parameter: allType IDENTIFIER;
//funcBody: '{' statement*  returnStmt '}'; //Can we put return stmt up in statement?
returnStmt:  'return ' expression;

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
   | arrayDimensions mapLayer                      #mapExpression
   | factor2 arrayDimensions					         #arrayAccess
   | factor2 '.' IDENTIFIER arrayDimensions			#mapAccess
   ;
factor2
	: IDENTIFIER                        #identifierAccess
  	| functionCall 						#functionAccess
	;

arrayDimensions: '[' expression (',' expression )* ']' ;

mapLayer: '{' individualLayer  (';' individualLayer)* '}' ;
individualLayer: identifierType IDENTIFIER ('=' expression)?;
//mapLayer: '{' identifierType IDENTIFIER ('=' expression)? (';' identifierType IDENTIFIER ('=' expression)?)* '}' ;

assignment: IDENTIFIER (arrayDimensions)? '=' expression ;

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

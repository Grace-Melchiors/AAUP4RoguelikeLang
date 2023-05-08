grammar Vesta;

program: line* EOF;

line: statement | ifBlock | whileBlock | chanceBlock | functionDclr | forLoop;

statement : (declaration | assignment | functionCall ) ';';

ifBlock: 'if' '(' expression ')' block ('else' elseIfBlock)?;

elseIfBlock: block | ifBlock;

whileBlock: 'while' '(' expression ')' block;

forLoop: 'for' '(' (declaration | assignment) ';' expression ';' assignment ')' block;

chanceBlock: 'chance' '{' (expression ':' block)+ '}';

functionDclr: identifierType IDENTIFIER '('(funcParams)?')' funcBody;
functionCall: IDENTIFIER '(' (expression (',' expression)*)? ')';

funcParams: parameter (',' parameter)* ;
parameter: identifierType IDENTIFIER;
funcBody: '{' line*  returnStmt '}';
returnStmt:  'return' expression';';

block: '{' line* '}';

assignment : baseAssignment | arrayAssignment | mapAssignment ;

baseAssignment: IDENTIFIER '=' expression;    
arrayAssignment: IDENTIFIER '[' expression (',' expression) ']' '=' expression ;
mapAssignment:  IDENTIFIER '.' IDENTIFIER ('[' expression (',' expression)']') '=' expression;




mapLayer: identifierType IDENTIFIER ('=' expression)?;

declaration: identifierType IDENTIFIER '=' expression  ;

expression
    : constant                              #constantExpression
    | IDENTIFIER                            #identifierExpression
    | '(' expression ')'                    #parenthesizedExpression
    | expression '[' expression (',' expression ']')* ']' #arrayIdentifierExpression
    | expression'.'IDENTIFIER               #mapGetLayerExpression
    | '{' (expression(','expression)*) '}'  #arrayExpression
    | '[' expression ',' expression ']' '{' mapLayer (';'mapLayer)* '}' #mapExpression
    | 'rand('expression ',' expression ')'  #randomExpression
    | functionCall                          #functionCallExpression
    | '!' expression                        #notExpression
    | '-' expression                        #negExpression
    | expression multOp expression          #multiplicationExpression
    | expression addOp expression           #additionExpression
    | expression compareOp expression       #compareExpression
    | expression boolOp expression          #booleanExpression
    ;



multOp: '*' | '/';
addOp: '+' | '-';
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=';
boolOp: BOOL_OPERATOR;


BOOL_OPERATOR: '&&' | '||' ;

constant: INTEGER | BOOL;

INTEGER:  [0-9]+ ;
BOOL: 'true' | 'false';

identifierType: TYPE ('[' expression (',' expression)* ']')?;

TYPE: 'int' | 'bool' | 'map';


IDENTIFIER: [a-zA-Z] [a-zA-Z0-9]*;

WS: [\t\r\n]+ -> skip;

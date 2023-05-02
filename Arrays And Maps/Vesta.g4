grammar Vesta;

program: line* EOF;

line: statement | ifBlock | whileBlock | chanceBlock;

statement : (declaration | assignment | functionCall ) ';';

ifBlock: 'if' '(' expression ')' block ('else' elseIfBlock)?;

elseIfBlock: block | ifBlock;

whileBlock: 'while' '(' expression ')' block;

chanceBlock: 'chance' '{' (expression ':' block)+ '}';





block: '{' line* '}';

assignment: IDENTIFIER '=' expression;    



declaration: (mapDeclaration | baseDeclaration); 



mapDeclaration: 'map[' expression ']['expression ']' IDENTIFIER '={' mapLayer (';'mapLayer)* '}';

mapLayer: identifierType IDENTIFIER ('=' expression)?;

baseDeclaration: identifierType IDENTIFIER '=' expression  ;


functionCall: IDENTIFIER '(' (expression (',' expression)*)? ')';



expression
    : constant                              #constantExpression
    | IDENTIFIER                            #identifierExpression
    | '(' expression ')'                    #parenthesizedExpression
    | expression '[' expression (',' expression)* ']'    #arrayIdentifierExpression
    | IDENTIFIER'.'IDENTIFIER               #mapGetLayerExpression
    | '{' (expression(','expression)*) '}'  #arrayExpression
    | 'rand('expression ',' expression ')'  #randomExpression
    | functionCall                          #functionCallExpression
    | '!' expression                        #notExpression
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

identifierType: TYPE ('[' expression (',' expression)* ']' )?;

TYPE: 'int' | 'bool' | 'map';







IDENTIFIER: [a-zA-Z] [a-zA-Z0-9]*;

WS: [\t\r\n]+ -> skip;

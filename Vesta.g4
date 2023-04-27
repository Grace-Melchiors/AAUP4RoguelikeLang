grammar Vesta;

program: line* EOF;

line: statement | ifBlock | whileBlock | chanceBlock;

statement: (declartion|assignment|functionCall) ';';

ifBlock: 'if' '(' expression ')' block ('else' elseIfBlock)?;

elseIfBlock: block | ifBlock;

whileBlock: 'while' '(' expression ')' block;

chanceBlock: 'chance' '{' (expression ':' block)+ '}';



block: '{' line* '}';

assignment: IDENTIFIER '=' expression;    
declartion: 'var' IDENTIFIER '=' expression;

functionCall: IDENTIFIER '(' (expression (',' expression)*)? ')';



expression
    : constant                              #constantExpression
    | IDENTIFIER                            #identifierExpression
    | IDENTIFIER ('[' expression ']')+      #arrayIdentifierExpression
    | '{' (expression(','expression)*) '}'  #arrayExpression
    | 'rand('expression ',' expression ')'  #randomExpression
    | functionCall                          #functionCallExpression
    | IDENTIFIER'.' arrayOp'('expression')' #arrayOperationExpression
    | '(' expression ')'                    #parenthesizedExpression
    | '!' expression                        #notExpression
    | expression multOp expression          #multiplicationExpression
    | expression addOp expression           #additionExpression
    | expression compareOp expression       #compareExpression
    | expression boolOp expression          #booleanExpression
    ;

arrayOp: 'remove' | 'add' ;
multOp: '*' | '/';
addOp: '+' | '-';
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=';
boolOp: BOOL_OPERATOR;


BOOL_OPERATOR: '&&' | '||' ;

constant: INTEGER | BOOL;

INTEGER: [0-9]+;
BOOL: 'true' | 'false';

identifierType: TYPE ('[' expression ']')*;

TYPE: 'int' | 'bool' | 'map';




IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;

WS: [\t\r\n]+ -> skip;




/*


*/
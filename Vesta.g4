grammar Vesta;

program: line* EOF;

line: statement | ifBlock | whileBlock | chanceBlock;

statement: (declaration|assignment|functionCall) ';';

ifBlock: 'if' '(' expression ')' block ('else' elseIfBlock)?;

elseIfBlock: block | ifBlock;

whileBlock: 'while' '(' expression ')' block;

chanceBlock: 'chance' '{' (expression ':' block)+ '}';



block: '{' line* '}';

assignment: IDENTIFIER '=' expression;    
declaration: 'var' IDENTIFIER '=' expression;

functionCall: IDENTIFIER '(' (expression (',' expression)*)? ')';



expression
    : '(' expression ')'                                  
    | logicalExpression                                         
    | arithmeticExpression                                      #arithmeticExpression
    | '{' (expression(','expression)*) '}'                      #arrayExpression
    | functionCall                                              #functionCallArithmeticExpression                              
    ;

arithmeticExpression
    : INTEGER                                                   #constantArithmeticExpression
    | IDENTIFIER                                                #identifierArithmeticExpression
    | IDENTIFIER ('[' arithmeticExpression ']')+                #arrayIdentifierArithmeticExpression
    | 'rand('arithmeticExpression ',' arithmeticExpression ')'  #randomArithmeticExpression
    | IDENTIFIER'.' arrayOp'('arithmeticExpression')'           #arrayOperationArithmeticExpression
    | arithmeticExpression multOp arithmeticExpression          #multiplicationArithmeticExpression
    | arithmeticExpression addOp arithmeticExpression           #additionArithmeticExpression
    ;

logicalExpression
    : BOOL                                                      #constantLogicalExpression
    | IDENTIFIER                                                #identifierLogicalExpression
    | IDENTIFIER ('[' arithmeticExpression ']')+                #arrayIdentifierLogicalExpression
    | functionCall                                              #functionCallLogicalExpression
    | IDENTIFIER'.' arrayOp'('logicalExpression')'              #arrayOperationLogicalExpression
    | '!' logicalExpression                                     #notLogicalExpression
    | arithmeticExpression compareOp arithmeticExpression       #compareLogicalExpression
    | logicalExpression boolOp logicalExpression                #booleanLogicalExpression
    ;

arrayOp: 'remove' | 'add' ;
multOp: '*' | '/';
addOp: '+' | '-';
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=';
boolOp: BOOL_OPERATOR;


BOOL_OPERATOR: '&&' | '||' ;

INTEGER: [0-9]+;
BOOL: 'true' | 'false';

identifierType: TYPE ('[' expression ']')*;

TYPE: 'int' | 'bool' | 'map';




IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;

WS: [\t\r\n]+ -> skip;




/*


*/
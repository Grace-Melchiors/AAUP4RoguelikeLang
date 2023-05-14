using Stdlib;

int w = 64;
int h = 64;
bool TRUE = true;
int[5] array = 5;

if (false || TRUE) {
    w = w+1;
} else {
    for (int i = 0; i < 5; i = i+1) {
        Stdlib.print(i);
        array[i] = i*2;
    }
}

{
    Stdlib.print(myFunc(w,h,true));
}

while (h > 10) {
    chance{
        32: { h = h / 4; }
        55: { h = h * 2; }
    }
    Stdlib.print(h);
}

chance{
    3: { Stdlib.print(3); }
    6: { Stdlib.print(6); }
}

for (int i = 0; i < 5; i = i+1) {
    Stdlib.print(array[i]);
}



int myFunc (int a, int b, bool test) {
    int result = 0;
    if (test && TRUE) {
        result = myFunc(a+1, b+1, false);
    } else {
        result = a * a + b * b;
    }
    return result;
}

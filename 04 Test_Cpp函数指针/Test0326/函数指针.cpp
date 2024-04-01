#include<iostream>
using namespace std;

void myFunction(int n,int m)
{
	cout << "this is test for n: " << n << " m:" << m << endl;
}


int main()
{
	void (*funp)(int,int);

	funp = &myFunction;

	(*funp)(12, 14);

}
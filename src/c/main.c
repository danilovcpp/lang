#include <stdio.h>
#include <stdlib.h>

#include "common.h"
#include "chunk.h"

/* run this program using the console pauser or add your own getch, system("pause") or input loop */

int main(int argc, char *argv[])
{
	Chunk chunk;
	initChunk(&chunk);
	writeChunk(&chunk, OP_RETURN);
	freeChunk(&chunk);

	return 0;
}

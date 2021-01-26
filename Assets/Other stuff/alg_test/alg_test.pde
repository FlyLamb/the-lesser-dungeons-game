int[] values;
color[] cfw = {color(0,0,0), color(127,55,22), color(127,127,127), color(255,0,0)};
//0 - nothing
//1 - crate wood
//2 - crate iron
//3 - enemy


// TECH BULLSHIT =============================================
void setup() {
  size(480,320);
  doRoom();
}

void doRoom() {
  
  noiseSeed(round(random(100000)));
  values = new int[96];
  for(int i = 0; i < 12; i++) {
    for(int j = 0; j < 8; j++) {
      values[i*8 + j] = alg(i,j,i*8+j);
    }
  }
}

void draw() {
  
  for(int i = 0; i < 12; i++) {
    for(int j = 0; j < 8; j++) {
      fill(cfw[values[i*8 + j]]);
      rect(i*40,j*40,40,40);
    }
  }
  
  if(key == 'r') doRoom();
  key = ' ';
}

//============================================================


//THE ALGHORITM THAT RETURNS THE TILE
int alg(int x, int y, int pos) {
  int v = round(noise(x * 3,y *3) * 12 + dist(6,4,x,y)*.2);
  if(v >= 0 && v <= 3) return v;
  return 0;
}

import ddf.minim.analysis.BeatDetect;
import ddf.minim.*;

Minim minim;  
//AudioInput input;
AudioPlayer input;
AudioMetaData meta;

BeatDetect beat;

PrintWriter output;

int[] beats;
int index;

void setup()
{
  size(100, 100);

  frameRate(44000);
  
  beats = new int[10];
  index = 0;

  minim = new Minim(this);

  input = minim.loadFile("test.mp3");
  meta = input.getMetaData();
  String filename = meta.author() + " - " + meta.title();
  
  beat = new BeatDetect();
  
  //////  Lärmenergiemodus:
  beat.detectMode(BeatDetect.SOUND_ENERGY);
  
  
  ////// Neue Datei erzeugen
  output = createWriter(filename + ".txt");
  
  
  input.play();
}

void draw()
{
  if(input.isPlaying()){
      
    beat.detect(input.mix);
  
    
    //////  Beat
    if (beat.isOnset()) {
      //println("Beat");
      
      //output.println(millis());
      beats[index] = millis();
      if(index > 0 && beats[index] - beats[index-1] > 500)
      {
        index++;
        //println(millis());
        chkArray();
      }
      else if(index < 1)
      {
        index++;
        //println(millis()); 
        chkArray();
      }

    }
    /*
    if (frameCount % 1000 == 0) {
      fill(0,0,0,32);
      rect(0,0,100,100);
    }*/
    //////
    
  }
  else
  {
    ende();
  }
}

void keyPressed() {
  ende();
}

void chkArray() {
  if(index == beats.length)
  {
    //println("Array vergroessern");
    int[] neuesArray = new int[beats.length * 2];
    
    for(int i = 0; i < beats.length; i++)
    {
      neuesArray[i] = beats[i];
    }
    
    beats = neuesArray;
  }
}

void ende() {
  input.close();
  
  ////  Laenge des Liedes in Millisekunden in erste Zeile schreiben
  output.println(input.length()); 
  
  ////  Anzahl der registrierten Beats in zweite Zeile schreiben, danach Leerzeile
  output.println(index);
  
  output.println();
  
  ////  Beats-Array in Datei schreiben
  for(int i = 0; i < index; i++)
  {
    output.println(beats[i]);
  }
  
  output.flush(); // Writes the remaining data to the file
  output.close(); // Finishes the file
  
  println(index + " Beats in Datei geschrieben.");
  
  exit(); // Stops the program
}

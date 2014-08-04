import ddf.minim.analysis.BeatDetect;
import ddf.minim.*;

Minim minim;  
//AudioInput input;
AudioPlayer input;
AudioMetaData meta;
int position, dauer;

BeatDetect beat;

PrintWriter output;

int[] beats;
int index;

////// GUI
PImage hintergrund;
PFont font32, font24;
int punkte = 0;

void setup()
{
  size(439, 146);
  
  //////  GUI init
  hintergrund = loadImage("hintergrund.jpg");
  font32 = loadFont("ComicJensFreePro-32.vlw");
  font24 = loadFont("ComicJensFreePro-24.vlw");
  textFont(font32, 32);

  frameRate(44000);
  
  //////  Beat-Detection init
  beats = new int[10];
  index = 0;

  minim = new Minim(this);

  input = minim.loadFile("song.mp3");
  meta = input.getMetaData();
//  String filename = meta.author() + " - " + meta.title();
  String filename = "analyse";
  dauer = input.length();
  position = 0;
  
  beat = new BeatDetect();
  
  //////  Lärmenergiemodus:
  beat.detectMode(BeatDetect.SOUND_ENERGY);
  
  
  ////// Neue Datei erzeugen
  output = createWriter(filename + ".txt");
  input.play();
}

void draw()
{
  
  if(input.isPlaying())
  {
    //////  Nur jeden 1000sten Frame zeichnen 
    if (frameCount % 1000 == 0)
    {
      image(hintergrund, 0, 0);
      fill(0);
      textFont(font32, 32);
      textAlign(LEFT, TOP);
      text("Lied wird analysiert", 50, 47);
      for(int i = 0; i < punkte; i++){
        text(".", 345 + i*10, 47);
      }
      textFont(font24, 24);
      textAlign(LEFT, BOTTOM);
      position = int(map(input.position(), 0, dauer, 0, 340));
      for(int i = 0; i < position; i+=5){
        text("|", 49 + i, 127);
      }
    }
      
    beat.detect(input.mix);
  
    
    //////  Beat
    if (beat.isOnset()) {
      //println("Beat");
      
      //output.println(millis());
      beats[index] = millis();
      if(index > 0 && beats[index] - beats[index-1] > 500)
      {
        index++;
        punkte = (punkte + 1)%4;
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
    
  }
  //////  Wenn Lied zu Ende, Programm schliessen
  else
  {
    ende();
  }
}

void chkArray() {
  if(index == beats.length)
  {
    //println("Array vergroessern");
    println(frameRate);
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
  output.println(); 
  
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
  
  println("exit");
  exit(); // Stops the program
}

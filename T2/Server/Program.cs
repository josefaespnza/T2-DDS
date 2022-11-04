using Server;

int mode = PreGame.AskGameMode();
Game game = new Game(mode);
game.Play();
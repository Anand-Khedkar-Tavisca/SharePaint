export class Stroke {
    Id:string;
    StartPoint: Point;
    EndPoint : Point;
    Lines: Line[];
    LinesQueue: Line[];
    
    IsUndone: boolean;
    constructor()
    {
        this.StartPoint = new Point();
        this.EndPoint = new Point();
        this.Lines = [];
        this.LinesQueue = [];
    }
}

export class Point {
    X:number;
    Y:number;
}

export class Line {
    StrokeId:string;
    StartPoint: Point;
    EndPoint : Point;
    constructor()
    {
        this.StartPoint = new Point();
        this.EndPoint = new Point();
    }
}

export class Paint{
      Id:string;
      Name : string;
     Strokes: Stroke[];
}
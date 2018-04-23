import { TestBed, inject } from '@angular/core/testing';
import { StrokeService } from './stroke.service';


describe('StrokeService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [StrokeService]
    });
  });

  it('should be created', inject([StrokeService], (service: StrokeService) => {
    expect(service).toBeTruthy();
  }));
});

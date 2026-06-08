import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';

import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AppComponent],
      providers: [
        provideRouter([])
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should render the header component', () => {
    const compiled = fixture.nativeElement as HTMLElement;

    expect(
      compiled.querySelector('app-header')
    ).toBeTruthy();
  });

  it('should render the footer component', () => {
    const compiled = fixture.nativeElement as HTMLElement;

    expect(
      compiled.querySelector('app-footer')
    ).toBeTruthy();
  });

  it('should render the router outlet', () => {
    const compiled = fixture.nativeElement as HTMLElement;

    expect(
      compiled.querySelector('router-outlet')
    ).toBeTruthy();
  });

  it('should render the main content container', () => {
    const compiled = fixture.nativeElement as HTMLElement;

    const contentContainer = compiled.querySelector(
      '.flex-1.overflow-auto'
    );

    expect(contentContainer).toBeTruthy();
  });

  it('should render exactly one header and one footer', () => {
    const compiled = fixture.nativeElement as HTMLElement;

    expect(
      compiled.querySelectorAll('app-header').length
    ).toBe(1);

    expect(
      compiled.querySelectorAll('app-footer').length
    ).toBe(1);
  });
});
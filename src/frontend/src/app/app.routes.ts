import { Routes } from '@angular/router';
import { NotFoundComponent } from './pages/not-found/not-found.component';

export const routes: Routes = [
    {
        path: 'optimizer',
        loadComponent: () => import('./pages/optimizer/optimizer.component').then(m => m.OptimizerComponent),
    },
    { path: '', redirectTo: '/optimizer', pathMatch: 'full' },
    { 
        path: '**', 
        loadComponent: () => import('./pages/not-found/not-found.component').then(m => m.NotFoundComponent) 
    },
];

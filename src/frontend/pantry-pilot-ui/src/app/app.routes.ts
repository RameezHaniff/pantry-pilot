import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'optimizer',
        loadComponent: () => import('./pages/optimizer/optimizer.component').then(m => m.OptimizerComponent),
    },
    {
        path: 'ingredients',
        loadComponent: () => import('./pages/ingredients/ingredients.component').then(m => m.IngredientsComponent),
    },
    { path: '', redirectTo: '/optimizer', pathMatch: 'full' },

];

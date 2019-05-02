import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailsComponent } from './members/member-details/member-details.component';
import { MemeberDetailResolver } from './_resolvers/member-details.resolver';
import { MemeberListResolver } from './_resolvers/member-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemeberEditResolver } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';


export const appRoures: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '', runGuardsAndResolvers: 'always', canActivate: [AuthGuard], children: [
            { path: 'members', component: MemberListComponent, resolve: { users: MemeberListResolver } },
            { path: 'members/:id', component: MemberDetailsComponent, resolve: { user: MemeberDetailResolver } },
            { path: 'messages', component: MessagesComponent },
            { path: 'lists', component: ListsComponent },
            {
                path: 'member/edit', component: MemberEditComponent,
                resolve: { user: MemeberEditResolver }, canDeactivate: [PreventUnsavedChanges]
            },
        ]
    },

    { path: '**', redirectTo: '', pathMatch: 'full' }

];

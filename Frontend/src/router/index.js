import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '@/views/home/HomeView.vue';
import LoginView from '@/views/LoginView.vue';
import RegisterView from '@/views/RegisterView.vue';
import ProfileView from '@/views/profile/ProfileView.vue';
import EditProfileView from '@/views/profile/EditProfileView.vue';

const routes = [
    { path: '/', name: 'home', component: HomeView },
    { path: '/login', name: 'login', component: LoginView },
    { path: '/register', name: 'register', component: RegisterView },
    { path: '/profile', name: 'profile', component: ProfileView },
    { path: '/edit-profile', name: 'edit-profile', component: EditProfileView }
];

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL), 
    routes
});

export default router;
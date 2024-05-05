import {createRouter, createWebHistory} from 'vue-router'
import Main from "@/views/main.vue";
import Menu from "@/views/menu.vue";
import Login from "@/views/login.vue";
import AdminPage from "@/views/admin-page.vue";
import axios from 'axios';
import {useUserStore} from "@/stores/user.js";
import AdminCreate from "@/views/admin-create.vue";
import AdminCategory from "@/views/admin-category.vue";
import AdminCreateCategory from "@/views/admin-create-category.vue";
import AdminUsers from "@/views/admin-users.vue";
import AdminTable from "@/views/admin-table.vue";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            alias: '/',
            path: '/main',
            name: 'main',
            component: Main
        },
        {
            path: '/menu',
            name: 'menu',
            component: Menu
        },
        {
            path: '/login',
            name: 'login',
            component: Login
        },
        {
            path: '/admin',
            name: 'admin',
            component: AdminPage,
            meta: {
                requiresAuth: true,
                roles: ['admin']
            }
        },
        {
            path: '/product-create',
            name: 'product-create',
            component: AdminCreate,
            meta: {
                requiresAuth: true,
                roles: ['admin']
            }
        },
        {
            path: '/create-category',
            name: 'create-category',
            component: AdminCreateCategory,
            meta: {
                requiresAuth: true,
                roles: ['admin']
            }
        },
        {
            path: '/category-admin',
            name: 'category-admin',
            component: AdminCategory,
            meta: {
                requiresAuth: true,
                roles: ['admin']
            }
        },
        {
            path: '/users',
            name: 'users',
            component: AdminUsers,
            meta: {
                requiresAuth: true,
                roles: ['admin']
            }
        },
        {
            path: '/tables',
            name: 'tables',
            component: AdminTable,
            meta: {
                requiresAuth: true,
                roles: ['admin']
            }
        },
        {
            path: '/forbidden',
            name: 'forbidden',
            component: Main
        }
    ]
});

router.beforeEach(async (to, from, next) => {

    const store = useUserStore();

    if (store.user === null) {
        const response = await axios.post('http://localhost:5178/api/Admin/CurrentUser', null, {withCredentials: true});
        store.setUser(response.data.data);
    }

    if (store.user !== null && to.name === 'login')
        return next('/');

    if (to.meta.requiresAuth && store.user === null)
        return next('/login')


    if ((to.meta.roles || []).length > 0) {

        if (store.user !== null && to.meta.roles.map(i => i.toLowerCase()).indexOf(store.user.role.toLowerCase()) >= 0)
            return next();
        else if (to.name !== 'forbidden')
            return next('/forbidden')

    } else
        return next();
})

export default router

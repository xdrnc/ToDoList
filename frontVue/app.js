const routes=[
    {path:'/home',component:home},
    {path:'/todolist',component:todolist}
]

const router=new VueRouter({
    routes
})

const app = new Vue({
    router
}).$mount('#app')
import {ref, computed} from 'vue'
import {defineStore} from 'pinia'

export const useUserStore = defineStore('user', () => {

    return {
        user: ref(null),
        setUser(user) {
            this.user = user;
        }
    }
})

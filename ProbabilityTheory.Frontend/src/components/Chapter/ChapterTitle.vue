<template>
    <h3 v-show="!editing">
        <MenuIcon data-target="slide-out" class="sidenav-trigger" style=" cursor: pointer"/> 
            {{ chapter.title }} 
        <EditIcon @click="editing=true" v-if="authorized" style=" cursor: pointer"/>
    </h3>
    <div v-show="editing" style="margin-top: 8px">
        <ChapterForm :chapter="chapter" v-on:done="toggleEditing"/>
    </div>

<SidenavMenu />
</template>

<script>
import ChapterForm from '@/components/Chapter/ChapterForm.vue'

import SidenavMenu from '@/components/common/SidenavMenu.vue'

import MenuIcon from 'icons/Menu'
import EditIcon from 'icons/Pencil'

export default {
    name: 'ChapterTitle',
    props: ['chapter'],
    components: {
        ChapterForm,
        SidenavMenu,
        MenuIcon, EditIcon
    },
    data() {
        return {
            editing: false
        }
    },
    computed: {
        authorized() { return true }
    },
    methods: {
        toggleEditing() {
            this.editing = !this.editing
        }
    }
}
</script>

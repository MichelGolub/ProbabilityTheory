<template>
    <header>
        <NavbarMenu />
    </header>
    <main>
        <router-view/>
    </main>
</template>

<script>
import NavbarMenu from "@/components/common/NavbarMenu.vue"

export default {
    name: "App",
    components: {
        NavbarMenu
    }
}

</script>

<style>
@import '~materialize-css/dist/css/materialize.min.css';
@import '~vue-material-design-icons/styles.css';


</style>
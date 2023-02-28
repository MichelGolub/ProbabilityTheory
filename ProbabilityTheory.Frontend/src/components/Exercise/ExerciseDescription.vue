<template>
    <div class="card-panel" v-show="!editing">
        <p class="flow-text"> 
            {{ description }} 
            <EditIcon v-if="authorized" style=" cursor: pointer" @click="editing = true"/>
        </p>
    </div>
    
    <div v-show="editing">
        <Form @submit="updateDescription" :initial-values="{description}">
            <div class="row">
                <ExerciseDescriptionField/>
            </div>
            <div class="row">
                <button class="waves-effect waves-light btn btn-form" type="submit">
                    <ConfirmIcon/> 
                    {{ $t('button.confirm' )}}
                </button>
                <button class="waves-effect waves-light btn btn-form grey darken-1" type="reset"
                @click="editing = false">
                    <CancelIcon/> 
                    {{ $t('button.cancel' )}}
                </button>
            </div>
        </Form>
    </div>
</template>

<script>
import { Form } from 'vee-validate'
import ExerciseDescriptionField from '@/components/Exercise/ExerciseDescriptionField.vue'

import EditIcon from 'icons/Pencil'
import ConfirmIcon from 'icons/Check'
import CancelIcon from 'icons/CloseCircle'

export default {
    name: 'ExerciseDescription',
    props: ['id', 'description'],
    emits: ['descriptionChanged'],
    components: {
        Form, ExerciseDescriptionField,
        EditIcon,
        ConfirmIcon, CancelIcon
    },
    data() {
        return {
            editing: false
        }
    },
    computed: {
        authorized() { return true },
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) },
    },
    methods: {
        updateDescription(values, { resetForm }) {
            this.$emit('descriptionChanged', values.description)
            this.editing = false
            resetForm()
        }
    }
}
</script>
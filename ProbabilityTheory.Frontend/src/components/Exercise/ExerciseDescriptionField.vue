<template>
    <div class="input-field col s6">
        <label for="description" class="active">{{ $t('exercise.field.description') }}</label>
        <Field as="textarea" name="description" :rules="descriptionRules"/>
        <ErrorMessage name="description" />
    </div>
</template>

<script>
import { Field, ErrorMessage } from 'vee-validate'
import { string, required } from 'yup';

export default {
    name: 'ExerciseDescriptionField',
    components: {
        Field, ErrorMessage
    },
    data() {
        return {
            descriptionRules: string()
                .required()
                .max(2500, this.$t('error.field.max', {num: 2500}))
        }
    }
}
</script>